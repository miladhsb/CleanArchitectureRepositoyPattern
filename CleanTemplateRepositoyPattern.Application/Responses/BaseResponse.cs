using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string TraceId { get; set; }
        public IEnumerable<ApplicationErrorResponse> Message { get; set; }
       
    }
}
