using CleanTemplateRepositoyPattern.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Configurations.ModelConfigurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(p => p.RoleDisplayName).HasMaxLength(100);

            builder.HasData(
               new ApplicationRole
               {
                   Id = Guid.Parse("cac43a6e-f7bb-4448-baaf-1add431ccbbf"),
                   Name = "Employee",
                   RoleDisplayName= "کاربر",
                   NormalizedName = "EMPLOYEE"
               },
               new ApplicationRole
               {
                   
                   Id = Guid.Parse("cbc43a8e-f7bb-4445-baaf-1add431ffbbf"),
                   Name = "SuperAdmin",
                   RoleDisplayName = "کاربر ارشد",
                   NormalizedName = "SUPERADMIN"
               });
        }
    }
}
