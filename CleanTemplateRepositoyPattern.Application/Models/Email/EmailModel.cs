using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Models.Email
{
    public class EmailModel
    {
        public string MailTo { get; set; }
        public string? MailFrom { get; set; }
        public string Subject { get; set; }
        public string MailContent { get; set; }
        public string? Displayname { get; set; }
       

            
    }
}
