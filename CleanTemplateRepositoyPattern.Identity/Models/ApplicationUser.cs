using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ?UserAddress { get; set; }
        public string? UserImage { get; set; }
        public string TokenSecurityKey { get; set; }
        public override string Email { get => base.Email; set => base.Email = value; }
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }
    }
}
