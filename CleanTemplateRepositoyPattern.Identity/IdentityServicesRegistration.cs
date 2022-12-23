using CleanTemplateRepositoyPattern.Application.Constants;
using CleanTemplateRepositoyPattern.Application.Contracts.Identity;
using CleanTemplateRepositoyPattern.Application.Contracts.Persistence.EntityFramework;
using CleanTemplateRepositoyPattern.Application.Models.Identity;
using CleanTemplateRepositoyPattern.Identity.Configurations;
using CleanTemplateRepositoyPattern.Identity.Models;
using CleanTemplateRepositoyPattern.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));


            #region Add Services
            services.AddScoped<IAuthService, AuthService>();

            #endregion



            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(DbConnectionStringName.IdentitySqlServer),
            x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));


            services.AddIdentity<ApplicationUser, ApplicationRole>(Option =>
            {
                Option.Password.RequireDigit = false;
                Option.Password.RequiredLength = 3;
                Option.Password.RequireLowercase = false;
                Option.Password.RequireNonAlphanumeric = false;
                Option.Password.RequireUppercase = false;
                Option.Password.RequiredUniqueChars = 0;
                Option.User.RequireUniqueEmail = true;
                Option.SignIn.RequireConfirmedEmail = true;
                Option.Lockout.MaxFailedAccessAttempts = 5;
                Option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            })
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
               .AddErrorDescriber<CustomIdentityError>(); ;


            #region AddAuthentication

            services.AddAuthentication(Option =>
            {
                Option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(Jwt =>
            {
               
                //کلیدی اصلی jwt
                // var secretkey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]);
               
                //رویدادها
                Jwt.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = async (Messagecontext) =>
                    {
                        
                        //var Usermanager = Messagecontext.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                        //var UserId = Messagecontext.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                        //secretkeyval = (await Usermanager.FindByIdAsync(UserId)).TokenSecurityKey;

                    },
                    OnTokenValidated = async (context) =>
                    {
                       
                        if (!context.HttpContext.User.HasClaim("Role", "Admin"))
                        {

                            //نمایش کد 200
                            // context.Success();

                            //تغییر استاتوس کد
                            //  context.HttpContext.Response.StatusCode=406;

                            // or
                            //نمایش پیام شکست عملیات 
                            // context.Fail("Fail request");

                            
                        }
                    }
                };

                //اجرای درخواست بدون حالت امن اچ تی پی اس
                Jwt.RequireHttpsMetadata = false;
                //ذخیره توکن
                Jwt.SaveToken = true;

                var validationParameters = new TokenValidationParameters
                {
                    //زمانی که ساخته میشود و تازمانی که معتبر است
                    ClockSkew = TimeSpan.Zero, // default: 5 min
                    //اجبار به توکن
                    RequireSignedTokens = true,
                    //بررسی امضا
                    ValidateIssuerSigningKey = true,

                    //بررسی زمان انقضا
                    RequireExpirationTime = true,
                    //بررسی عمر توکن
                    ValidateLifetime = true,
                    //ولید کردن استفاده کننده
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtSettings:Audience"],

                    ValidateIssuer = true, //default : false
                    //نام صادر کننده
                    ValidIssuer = configuration["JwtSettings:Issuer"],

                };
               
                //ولیدشن توکن بر اساس کلید امنیتی موجود در جدول کاربران
                var Usermanager = services.BuildServiceProvider().GetRequiredService<ApplicationIdentityDbContext>();
                Jwt.SecurityTokenValidators.Clear();
                Jwt.SecurityTokenValidators.Add(new DynamicJwtValidationHandler(configuration.GetConnectionString(DbConnectionStringName.IdentitySqlServer)));
                
                //دادن پارامتر های اعتبار سنجی توکن که بالا تعریف شده
                Jwt.TokenValidationParameters = validationParameters; 
            });


            #endregion


            #region ایجاد مایگریشن در صورتی که در دیتابیس اعمال نشده باشد
            using (var servicescope = services.BuildServiceProvider().CreateScope())
            {
                var dbcontext = servicescope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();


                if (dbcontext.Database.GetPendingMigrationsAsync().Result.Any())
                {
                    dbcontext.Database.MigrateAsync().Wait();
                }
            }
            #endregion

            return services;
        }
    }
}
