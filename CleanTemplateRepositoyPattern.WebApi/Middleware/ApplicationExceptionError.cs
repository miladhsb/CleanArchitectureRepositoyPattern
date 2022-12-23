using CleanTemplateRepositoyPattern.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.WebApi.Middleware
{
    public class ApplicationExceptionError
    {

        public string TraceId { get; set; }
        public IEnumerable<ApplicationErrorResponse> ErrorMessages { get; set; }
        public string HelpLink { get; set; }
        public string ExceptionType { get; set; }
        public string ExceptionSource { get; set; }
        public string MemberName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string HttpRoute { get; set; }
    }
}
