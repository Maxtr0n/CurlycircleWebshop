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
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseHiLo("orderitemseq");

            builder.HasOne(o => o.Order)
                .WithMany(nameof(Order.OrderItems))
                .HasForeignKey(nameof(OrderItem.OrderId))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
