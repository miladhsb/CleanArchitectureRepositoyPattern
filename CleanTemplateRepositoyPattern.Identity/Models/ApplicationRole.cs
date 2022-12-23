using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.Identity.Models
{
    public class ApplicationRole:IdentityRole<Guid>
    {

        public string? RoleDisplayName { get; set; }
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public override string Name { get => base.Name; set => base.Name = value; }
        public override string NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
    }
}
