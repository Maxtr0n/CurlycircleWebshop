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
    public class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> cartConfiguration)
        {
            cartConfiguration.ToTable("Carts");

            cartConfiguration.HasKey(c => c.Id);

            cartConfiguration.Property(c => c.Id)
                .UseHiLo("cartseq");

            cartConfiguration.OwnsMany(c => c.CartItems, ci =>
            {
                ci.Property(ci => ci.Id).UseHiLo("cartitemseq");
                ci.HasKey(ci => ci.Id);
                ci.WithOwner(ci => ci.Cart).HasForeignKey(ci => ci.CartId);
                ci.Navigation(ci => ci.Cart).UsePropertyAccessMode(PropertyAccessMode.Field);
                ci.HasOne(ci => ci.Product);
                ci.Navigation(ci => ci.Product).AutoInclude();
            });
        }
    }
}
