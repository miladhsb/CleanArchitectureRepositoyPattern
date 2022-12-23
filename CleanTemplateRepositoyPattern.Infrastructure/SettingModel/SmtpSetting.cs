using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Infrastructure.SettingModel
{
    public class SmtpSetting
    {
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; } 
        public string SMTPServer { get; set; } 
        public int SMTPPort { get; set; } 
        public bool SMTPSSl { get; set; }
    }
}
