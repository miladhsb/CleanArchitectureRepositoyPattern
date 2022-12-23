using CleanTemplateRepositoyPattern.Application.Contracts.Email;
using CleanTemplateRepositoyPattern.Application.Models.Email;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using CleanTemplateRepositoyPattern.Infrastructure.SettingModel;
using CleanTemplateRepositoyPattern.Application.Responses;

namespace CleanTemplateRepositoyPattern.Infrastructure.Mail
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSetting _smtpOptions;

        public SmtpEmailSender(IOptions<SmtpSetting> SmtpOptions)
        {
            _smtpOptions = SmtpOptions.Value;
        }
        public async Task<BaseResponse> SendEmail(EmailModel email)
        {
            try
            {
                NetworkCredential credential = new NetworkCredential()
                {
                    UserName = _smtpOptions.SMTPUserName,
                    Password = _smtpOptions.SMTPPassword
                };
                // Command-line argument must be the SMTP host.
                SmtpClient client = new SmtpClient()
                {
                    Host = _smtpOptions.SMTPServer,
                    Credentials = credential,
                    Port = _smtpOptions.SMTPPort,
                    EnableSsl = _smtpOptions.SMTPSSl

                };
                MailAddress from = new MailAddress(email.MailFrom,
                   email.Displayname,
                System.Text.Encoding.UTF8);
                // Set destinations for the email message.
                MailAddress to = new MailAddress(email.MailTo);



                using MailMessage message = new MailMessage(from, to);
                message.Body = email.MailContent;
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = email.Subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                //اتچ فایل
                //message.Attachments.Add(data);

                client.SendCompleted += new
                SendCompletedEventHandler(CompletedMailRes);

                await client.SendMailAsync(message);

                return ResponseFactory.CreateBaseResponseSuccess("ایمیل با موفقیت ارسال شد");
                //message.Dispose();
            }
            catch (Exception e)
            {
                return ResponseFactory.CreateBaseResponsefailed(e.Message);

            }
           

        }

        void CompletedMailRes(object sender, AsyncCompletedEventArgs e)
        {
            var ee = e;
        }

       
    }
}
