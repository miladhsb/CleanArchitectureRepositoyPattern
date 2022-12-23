using CleanTemplateRepositoyPattern.Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations.EntityConfigurations
{
    public class BlugPostConfiguration:BaseEntityConfiguration<BlogPost>
    {
        public override void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            

            builder.Property(p => p.Body).IsRequired();
            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(p => p.MetaKeywords).HasMaxLength(300).IsRequired(false);
            builder.Property(p => p.MetaTitle).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Tags).HasMaxLength(300).IsRequired();
            builder.Property(p => p.PostImage).HasMaxLength(300).IsRequired(false);
            builder.Property(p => p.PostSlug).HasMaxLength(150).IsRequired();
            builder.HasMany(p => p.blogComments).WithOne(p => p.blogPost).HasForeignKey(p=>p.BlogPostId);
            base.Configure(builder);
        }
    }
}
