
using CleanTemplateRepositoyPattern.Application.Contracts.Email;
using CleanTemplateRepositoyPattern.Application.Models.Email;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplateRepositoyPattern.WebApi.Controllers
{
   
    public class Email : BaseController
    {
        private readonly IEmailSender _emailSender;

        public Email(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel loginRequest)
        {
            var Emailresult = await _emailSender.SendEmail(loginRequest);
            return Ok(Emailresult);
        }
    }
}
