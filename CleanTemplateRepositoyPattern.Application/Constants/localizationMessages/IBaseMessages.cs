using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Constants.localizationMessages
{
    public interface IBaseMessages
    {
        string UserNotFound { get; }
        string AccountLocked { get; }
        string UserIsNotAllowed { get; }
        string RequiresTwoFactor { get; }
        string SuccessUserLogin { get; }
        string UserLoginFailed { get; }
        string RoleNotFound { get; }
        string RoleAddClaimsSuccess { get; }
        string RoleAddClaimsFailed { get; }

        string SuccessRegisterUser(string UserName);
        string SuccessUserAddRole(string Role, string UserName);
        string UserExists(string UserName);
    }
}
