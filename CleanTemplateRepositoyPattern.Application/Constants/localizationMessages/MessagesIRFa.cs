using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Constants.localizationMessages
{
    public class MessagesIRFa: IBaseMessages
    {
        public string UserNotFound => "کاربر یافت نشد";
        public string AccountLocked => "حساب کاربری قفل شده است";
        public string UserIsNotAllowed => "کاربر مجاز به ورود نمیباشد";
        public string RequiresTwoFactor => "حساب کاربری دارای رمز دوعاملی است";
        public string UserExists(string UserName) => $"کاربر با نام کاربری {UserName} وجود دارد";
        public string SuccessRegisterUser(string UserName)=> $"کاربر با نام کاربری {UserName} با موفقیت ثبت گردید";
        public string SuccessUserAddRole(string Role, string UserName) => "نقش {Role} باموفقیت به کاربر {UserName} اضافه شد";
        public string SuccessUserLogin => "ورود با موفقیت انجام شد";
        public string UserLoginFailed => "ورود ناموفق نام کاربری یا کلمه عبود نادرست است";

        public string RoleNotFound => "نقش یافت نشد";

        public string RoleAddClaimsSuccess => "تمام نقش ها اضافه شدند";
        public string RoleAddClaimsFailed => "ثبت برخی از نقش ها با مشکل مواجه شد";
    }
}
