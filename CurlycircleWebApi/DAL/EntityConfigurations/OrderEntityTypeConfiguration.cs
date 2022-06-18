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
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> orderConfiguration)
        {
            orderConfiguration.ToTable("Orders");

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Property(o => o.Id)
                .UseHiLo("orderseq");

            orderConfiguration.Property<int>(o => o.OrderNumber)
                .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");

            orderConfiguration.OwnsMany(o => o.OrderItems, oi =>
            {
                oi.Property(oi => oi.OrderId).UseHiLo("orderitemseq");
                oi.HasKey(oi => oi.Id);
                oi.WithOwner(oi => oi.Order).HasForeignKey(oi => oi.OrderId);
                oi.Navigation(oi => oi.Order).UsePropertyAccessMode(PropertyAccessMode.Field);
                oi.HasOne(oi => oi.Product);
                oi.Navigation(oi => oi.Product).AutoInclude();
            });

            orderConfiguration.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner(a => a.Order).HasForeignKey(a => a.OrderId);
                a.Property(a => a.ZipCode).HasColumnName("ZipCode");
                a.Property(a => a.City).HasColumnName("City");
                a.Property(a => a.Line1).HasColumnName("Line1");
                a.Property(a => a.Line2).HasColumnName("Line2");
            });
        }
    }
}
