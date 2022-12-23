using CleanTemplateRepositoyPattern.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Exceptions
{
    public class IdentityResultException: ApplicationException
    {
        public List<ApplicationErrorResponse> Errors { get; set; } = new List<ApplicationErrorResponse>();

        public IdentityResultException(List<ApplicationErrorResponse> applicationError)
        {
            Errors = applicationError;
        }
    }
}
