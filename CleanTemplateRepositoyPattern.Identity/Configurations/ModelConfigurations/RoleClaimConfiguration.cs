using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Configurations.ModelConfigurations
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
        {
            builder.HasData(

                new IdentityRoleClaim<Guid>() { Id=1,ClaimType=ClaimTypes.Role, ClaimValue= "ADD", RoleId=Guid.Parse("CBC43A8E-F7BB-4445-BAAF-1ADD431FFBBF") },
                new IdentityRoleClaim<Guid>() { Id=2,ClaimType = ClaimTypes.Role, ClaimValue = "Delete", RoleId = Guid.Parse("CBC43A8E-F7BB-4445-BAAF-1ADD431FFBBF") }

                );
        }
    }
}
