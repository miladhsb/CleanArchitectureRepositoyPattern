using CleanTemplateRepositoyPattern.Application.Models.Identity;
using CleanTemplateRepositoyPattern.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<DataResponse<LoginResponce>> LoginAsync(LoginRequest loginRequest);
        Task<DataResponse<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest);
        Task<BaseResponse> UserAddRoleAsync(string UserId, string RoleName);
    }
}
