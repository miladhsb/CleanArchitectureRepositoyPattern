using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Responses
{
    public class DataResponse<TData>:BaseResponse
    {
        public TData? Data { get; set; }
    }
}
