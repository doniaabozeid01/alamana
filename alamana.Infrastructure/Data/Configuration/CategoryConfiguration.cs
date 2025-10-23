using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alamana.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace alamana.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.Property(c => c.NameEn)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.NameAr)
                   .IsRequired()
                   .HasMaxLength(150);


            builder.Property(c => c.DescriptionEn)
                   .HasMaxLength(500);



            builder.Property(c => c.DescriptionAr)
                   .HasMaxLength(500);



            builder.Property(c => c.Slug)
                   .HasMaxLength(180);

            // Unique على Name
            builder.HasIndex(c => c.NameEn).IsUnique();
            builder.HasIndex(c => c.NameAr).IsUnique();

            // Unique على Slug (مع تجاهل nulls)
            builder.HasIndex(c => c.Slug)
                   .IsUnique()
                   .HasFilter("[Slug] IS NOT NULL"); // لـ SQL Server. لو PostgreSQL: .HasFilter("\"Slug\" IS NOT NULL")


        }
    }
}
