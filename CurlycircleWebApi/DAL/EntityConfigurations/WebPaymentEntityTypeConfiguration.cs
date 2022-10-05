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
    public class WebPaymentEntityTypeConfiguration : IEntityTypeConfiguration<WebPayment>
    {
        public void Configure(EntityTypeBuilder<WebPayment> builder)
        {
            builder.ToTable("WebPayments");

            builder.HasKey(wp => wp.Id);

            builder.Property(wp => wp.Id)
                .UseHiLo("webpaymentseq");
        }
    }
}
