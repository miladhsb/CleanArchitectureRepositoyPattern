using CleanTemplateRepositoyPattern.Application.Constants;
using CleanTemplateRepositoyPattern.Application.Constants.localizationMessages;
using CleanTemplateRepositoyPattern.Application.Contracts.Identity;
using CleanTemplateRepositoyPattern.Application.Exceptions;
using CleanTemplateRepositoyPattern.Application.Models.Identity;
using CleanTemplateRepositoyPattern.Application.Responses;
using CleanTemplateRepositoyPattern.Application.Utility;
using CleanTemplateRepositoyPattern.Identity.Configurations;
using CleanTemplateRepositoyPattern.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseMessages _baseMessages;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationIdentityDbContext _identityDbContext;

        public AuthService(IBaseMessages baseMessages, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<JwtSettings> jwtSettings, ApplicationIdentityDbContext identityDbContext)
        {
            this._baseMessages = baseMessages;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._jwtSettings = jwtSettings.Value;
            this._identityDbContext = identityDbContext;
        }


        public async Task<DataResponse<LoginResponce>> LoginAsync(LoginRequest loginRequest)
        {
            ApplicationUser applicationUser;

            if (loginRequest == null)
            {
                throw new ArgumentNullException("loginRequest cannot be null");
            }
            if (!string.IsNullOrEmpty(loginRequest.MobileNumber))
            {
                applicationUser = await _identityDbContext.Users.FirstOrDefaultAsync(p => p.PhoneNumber == loginRequest.MobileNumber);
            }
            else
            {
                applicationUser = await _userManager.FindByEmailAsync(loginRequest.Email);
            }

            if (applicationUser == null)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.UserNotFound, new LoginResponce());
            }

            var SignInresult = await _signInManager.CheckPasswordSignInAsync(applicationUser, loginRequest.Password, true);

            if (SignInresult.IsLockedOut)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.AccountLocked, new LoginResponce());

            }
            if (SignInresult.IsNotAllowed)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.UserIsNotAllowed, new LoginResponce());

            }

            if (SignInresult.RequiresTwoFactor)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.RequiresTwoFactor, new LoginResponce());

            }

            if (!SignInresult.Succeeded)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.UserLoginFailed, new LoginResponce());

            }


            //var Token = new JwtSecurityTokenHandler().WriteToken(await GenerateToken(applicationUser));
            var Token = new JwtSecurityTokenHandler().WriteToken(await GenerateTokenCompress(applicationUser));



            return ResponseFactory.CreateDataResponseSuccess(_baseMessages.SuccessUserLogin, new LoginResponce()
            {
                Email = applicationUser.Email,
                Id = applicationUser.Id.ToString(),
                UserName = applicationUser.UserName,
                Token = Token
            });


        }

        public async Task<DataResponse<RegistrationResponse>> RegisterAsync(RegistrationRequest registrationRequest)
        {


            var existingUser = await _userManager.FindByNameAsync(registrationRequest.UserName);

            if (existingUser != null)
            {
                return ResponseFactory.CreateDataResponsefailed(_baseMessages.UserExists(registrationRequest.UserName), new RegistrationResponse()
                {
                    UserName = existingUser.UserName
                });
            }

            var user = new ApplicationUser
            {
                Email = registrationRequest.Email,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                UserName = registrationRequest.UserName,
                PhoneNumber = registrationRequest.PhonNumber,
                UserAddress = registrationRequest.Address,
                UserImage = registrationRequest.UserImage,
                TokenSecurityKey = Guid.NewGuid().ToString().StringToBase64(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var Registerresult = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (Registerresult.Succeeded == false)
            {
                throw new IdentityResultException(Registerresult.Errors.Select(p => new ApplicationErrorResponse { Code = p.Code, Description = p.Description }).ToList());
            }

            existingUser = await _userManager.FindByNameAsync(registrationRequest.UserName);
            var AddRoleResult = await UserAddRoleAsync(existingUser.Id.ToString(), "Employee");




            return ResponseFactory.CreateDataResponseSuccess(_baseMessages.SuccessRegisterUser(existingUser.UserName), new RegistrationResponse()
            {
                UserName = existingUser.UserName
            });
        }


        public async Task<BaseResponse> UserAddRoleAsync(string UserId, string RoleName)
        {
            var applicationUser = await _userManager.FindByIdAsync(UserId);

            if (applicationUser == null)
            {
                throw new NotFoundException(applicationUser.UserName);
            }


            var RoleResult = await _userManager.AddToRoleAsync(applicationUser, RoleName);

            if (!RoleResult.Succeeded)
            {
                throw new IdentityResultException(RoleResult.Errors.Select(p => new ApplicationErrorResponse { Code = p.Code, Description = p.Description }).ToList());
            }


            return ResponseFactory.CreateBaseResponseSuccess(_baseMessages.SuccessUserAddRole(RoleName, applicationUser.UserName));
        }



        public async Task<BaseResponse> RoleAddClaimsAsync(string RoleId, List<ApplicationClaim> RequestClaims)
        {

            var ApplicationRole = await _roleManager.FindByIdAsync(RoleId);
            if (ApplicationRole == null)
            {
                throw new NotFoundException(ApplicationRole.Name);
            }




            var AllRoleClaim = await _roleManager.GetClaimsAsync(ApplicationRole);
            if (AllRoleClaim != null && AllRoleClaim.Count() != 0)
            {

                foreach (var item in AllRoleClaim)
                {
                    await _roleManager.RemoveClaimAsync(ApplicationRole, item);
                }


            }


            foreach (var item in RequestClaims)
            {
                var RoleClaimResult = await _roleManager.AddClaimAsync(ApplicationRole, new Claim(item.ClaimType, item.ClaimValue));

                if (!RoleClaimResult.Succeeded)
                {
                    throw new IdentityResultException(RoleClaimResult.Errors.Select(p => new ApplicationErrorResponse { Code = p.Code, Description = p.Description }).ToList());

                }
            }

            return ResponseFactory.CreateBaseResponseSuccess(_baseMessages.RoleAddClaimsSuccess);

        }





        #region GenerateToken Method

        //ایجاد توکن جدید
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {


            var claimsprensopal = await _signInManager.ClaimsFactory.CreateAsync(user);
            var Userclaims = claimsprensopal.Claims.ToList();

            var AllClaims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Typ, user.UserName),
                new Claim(CustomClaimTypes.FirstName, user.FirstName),
                new Claim(CustomClaimTypes.LastName, user.FirstName),
                new Claim(CustomClaimTypes.UserImage, user.FirstName),

            }
            .Union(Userclaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(user.TokenSecurityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: AllClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private async Task<SecurityToken> GenerateTokenCompress(ApplicationUser user)
        {


            var claimsprensopal = await _signInManager.ClaimsFactory.CreateAsync(user);
            var Userclaims = claimsprensopal.Claims.ToList();

            var AllClaims = new[]
            {
                //new Claim(JwtRegisteredClaimNames.Typ, user.UserName),
                new Claim(CustomClaimTypes.FirstName, user.FirstName),
                new Claim(CustomClaimTypes.LastName, user.FirstName),
                new Claim(CustomClaimTypes.UserImage, user.FirstName),

            }
            .Union(Userclaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(user.TokenSecurityKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                //دریافت کلایم ها
                Subject = new ClaimsIdentity(AllClaims),
                //ایجاد کننده
                Issuer = _jwtSettings.Issuer,
                //استفاده کننده
                Audience = _jwtSettings.Audience,
                ////زمان ایجاد شدن
                //IssuedAt = DateTime.Now,
                ////زمان آماده به کارشدن توکن
                ////اگر زمان فعلی زمان آماده به کار باشد نیازی به نوشتن نیست
                //NotBefore = DateTime.Now,
                //زمان انقضا
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                //کلید ساخته  شده در بالا
                SigningCredentials = signingCredentials,

                CompressionAlgorithm = CompressionAlgorithms.Deflate,
            };

            //ساخت توکن
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(descriptor);

            return securityToken;
        }

        #endregion

    }
}
