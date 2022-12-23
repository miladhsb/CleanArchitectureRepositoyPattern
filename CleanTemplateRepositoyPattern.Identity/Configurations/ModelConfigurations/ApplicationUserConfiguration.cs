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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.UserAddress).HasMaxLength(200);
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.LastName).HasMaxLength(100);
            builder.Property(p => p.UserImage).HasMaxLength(200);


            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb9"),
                     Email = "superadmin@localhost.com",
                     NormalizedEmail = "SUPERADMIN@LOCALHOST.COM",
                     FirstName = "سیستم",
                     LastName = "کاربر ارشد",
                     UserName = "superadmin",
                     PhoneNumber = "09121244567",
                     NormalizedUserName = "SUPERADMIN",
                     PasswordHash = hasher.HashPassword(null, "1234"),
                     SecurityStamp= "a4366774-293d-479a-b5a3-be98f096f0a0",
                     TokenSecurityKey= "C4366774-293d-479a-A5a3-be98f096f0a0",
                     EmailConfirmed = true
                 },
                 new ApplicationUser
                 {
                     Id = Guid.Parse("9e224968-33e4-4652-b7b7-8574d048cdb9"),
                     Email = "user@localhost.com",
                     NormalizedEmail = "USER@LOCALHOST.COM",
                     FirstName = "کاربر",
                     LastName = "سایت",
                     PhoneNumber="09121234567",
                     UserName = "user@localhost.com",
                     NormalizedUserName = "USER@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "123"),
                     SecurityStamp = "57eefa2d-e9ab-4adc-8557-23a53fcc9697",
                     TokenSecurityKey = "T4366774-293d-679a-A5a3-be98f096f0a0",
                     EmailConfirmed = true
                 });


        }
    }
}
