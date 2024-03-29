﻿using DAL.EntityConfigurations;
using DAL.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Color> Colors => Set<Color>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Pattern> Patterns => Set<Pattern>();
        public DbSet<WebPayment> WebPayments => Set<WebPayment>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Order>()
                .Property(o => o.PaymentMethod)
                .HasConversion<string>();

            modelBuilder
                .Entity<Order>()
                .Property(o => o.ShippingMethod)
                .HasConversion<string>();

            modelBuilder
                .Entity<WebPayment>()
                .Property(wp => wp.PaymentStatus)
                .HasConversion<string>();

            modelBuilder.HasSequence<int>("OrderNumbers")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("cartseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("orderseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("productcategoryseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("productseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("cartitemseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("orderitemseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("colorseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("materialseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("patternseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("webpaymentseq")
               .StartsAt(1000)
               .IncrementsBy(1);

            modelBuilder.ApplyConfiguration(new ApplicationUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CartEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ColorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PatternEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WebPaymentEntityTypeConfiguration());

            modelBuilder.Seed();
        }
    }
}