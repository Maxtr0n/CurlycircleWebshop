﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigurations
{
    public class ApplicationUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> userConfiguration)
        {
            userConfiguration.ToTable("Users");
            userConfiguration.HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Cart>(c => c.ApplicationUserId);
            userConfiguration.OwnsOne(u => u.Address, a =>
            {
                a.WithOwner(a => a.User).HasForeignKey(a => a.UserId);
                a.Property(a => a.ZipCode).HasColumnName("ZipCode");
                a.Property(a => a.City).HasColumnName("City");
                a.Property(a => a.Line1).HasColumnName("Line1");
                a.Property(a => a.Line2).HasColumnName("Line2");
            });
        }
    }
}
