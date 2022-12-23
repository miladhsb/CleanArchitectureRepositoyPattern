using CleanTemplateRepositoyPattern.Application.Models.Email;
using CleanTemplateRepositoyPattern.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Email
{
    public interface IEmailSender
    {
        Task<BaseResponse> SendEmail(EmailModel email);
    }
}
