using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}
