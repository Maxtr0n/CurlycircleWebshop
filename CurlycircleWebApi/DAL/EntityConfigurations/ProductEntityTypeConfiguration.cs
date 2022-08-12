using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseHiLo("productseq");

            builder.HasOne(p => p.ProductCategory)
                .WithMany(nameof(ProductCategory.Products))
                .HasForeignKey(nameof(Product.ProductCategoryId))
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(p => p.Colors).AutoInclude();
            builder.Navigation(p => p.Material).AutoInclude();
            builder.Navigation(p => p.Pattern).AutoInclude();

            builder.HasMany(p => p.Colors)
                .WithMany(c => c.Products);
            builder.HasOne(p => p.Material)
                .WithMany(m => m.Products);
            builder.HasOne(p => p.Pattern)
                .WithMany(p => p.Products);
        }
    }
}
