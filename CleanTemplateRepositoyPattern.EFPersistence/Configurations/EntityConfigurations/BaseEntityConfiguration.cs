using CleanTemplateRepositoyPattern.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplateRepositoyPattern.EFPersistence.Configurations.EntityConfigurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(p => p.IsDeleted == false);

            builder.HasKey(p => p.Id); 
            builder.Property(p => p.CreatedDate).HasColumnType("DateTime2");
            builder.Property(p => p.LastModifiedDate).HasColumnType("DateTime2").IsRequired(false);
            builder.Property(p => p.LastModifiedBy).IsRequired(false);

        }
    }
}
