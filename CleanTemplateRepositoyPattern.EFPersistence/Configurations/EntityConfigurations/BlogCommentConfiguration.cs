using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations.EntityConfigurations
{
    public class BlogCommentConfiguration: BaseEntityConfiguration<BlogComment>
    {
        public override void Configure(EntityTypeBuilder<BlogComment> builder)
        {
           //builder.OwnsOne(c => c.FullName).Property(c => c.Firstname).HasColumnName("Firstname");
            builder.Property(p=>p.CommentText).HasMaxLength(200).IsRequired();
            builder.Property(p => p.UserId);
            base.Configure(builder);
        }
    }
}
