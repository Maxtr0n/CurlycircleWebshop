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

            cartConfiguration.OwnsMany(c => c.OrderItems);
        }
    }
}
