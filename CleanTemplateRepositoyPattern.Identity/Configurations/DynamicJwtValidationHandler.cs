using CleanTemplateRepositoyPattern.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Configurations
{
    public class DynamicJwtValidationHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        private readonly string connection;

        public DynamicJwtValidationHandler(string connection)
        {
            this.connection = connection;
        }

        private SecurityKey GetUserTokenSecurityKey(string UserId)
        {
            DbContextOptionsBuilder<ApplicationIdentityDbContext> optionsBuilder=new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
            optionsBuilder.UseSqlServer(connection);
            using ApplicationIdentityDbContext db = new ApplicationIdentityDbContext(optionsBuilder.Options);
            var user = db.Users.FirstOrDefault(p=>p.Id==Guid.Parse(UserId));
            if (user == null)
                throw new Exception("User Id not found");
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(user.TokenSecurityKey));
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            // Read the token before starting validation
            JwtSecurityToken incomingToken = ReadJwtToken(token);
            // Extract external system ID from the token
            string UserId = incomingToken.Claims.First(claim => claim.Type == "nameid").Value;
            // Set up the Security Key for that user
            validationParameters.IssuerSigningKey = GetUserTokenSecurityKey(UserId);
            // Framework default validation
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
