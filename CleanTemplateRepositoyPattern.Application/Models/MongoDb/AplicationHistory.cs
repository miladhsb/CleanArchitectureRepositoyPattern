using CleanTemplateRepositoyPattern.Application.Models.MongoDb.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Models.MongoDb
{
    public class AplicationHistory:BaseModelMongo
    {
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string log { get; set; }
    }
}
