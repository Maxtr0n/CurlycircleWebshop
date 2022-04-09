﻿using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Users
            var users = new List<ApplicationUser>();
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@admin.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "abc123");
            users.Add(admin);

            var user1 = new ApplicationUser
            {
                Id = 2,
                UserName = "user",
                Email = "user@user.com",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            user1.PasswordHash = passwordHasher.HashPassword(user1, "abc123");
            users.Add(user1);

            modelBuilder.Entity<ApplicationUser>()
                .HasData(users);

            #endregion

            #region Roles
            modelBuilder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

            modelBuilder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                });

            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                });

            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>
                {
                    RoleId = 2,
                    UserId = 2
                });
            #endregion

            #region Carts

            var cart1 = new Cart
            {
                Id = 1,
                ApplicationUserId = 2,
            };

            modelBuilder.Entity<Cart>()
                .HasData(cart1);

            #endregion

            #region ProductCategories

            var productCategory1 = new ProductCategory
            {
                Id = 1,
                Name = "Curly hajgöndörítők",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
            };
            var productCategory2 = new ProductCategory
            {
                Id = 2,
                Name = "Hajcsatok",
                Description = "Donec tincidunt nunc ac sapien blandit pellentesque. Nulla tincidunt dui vitae nibh aliquet, et efficitur dui dignissim.",
            };
            var productCategory3 = new ProductCategory
            {
                Id = 3,
                Name = "Hajgumik",
                Description = "Vestibulum aliquam gravida dui, ut volutpat nisi semper quis. ",
            };
            modelBuilder.Entity<ProductCategory>()
                .HasData(
                    productCategory1,
                    productCategory2,
                    productCategory3
                );

            #endregion

            #region Products

            var product1 = new Product
            {
                Id = 1,
                Name = "Curly1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 2500,
                ProductCategoryId = 1,
            };
            var product2 = new Product
            {
                Id = 2,
                Name = "Curly2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 3000,
                ProductCategoryId = 1,
            };
            var product3 = new Product
            {
                Id = 3,
                Name = "Curly3",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 3000,
                ProductCategoryId = 1,
            };
            var product4 = new Product
            {
                Id = 4,
                Name = "Curly4",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 3500,
                ProductCategoryId = 1,
            };
            var product5 = new Product
            {
                Id = 5,
                Name = "Curly5",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 3500,
                ProductCategoryId = 1,
            };
            var product6 = new Product
            {
                Id = 6,
                Name = "Hajcsat1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 4000,
                ProductCategoryId = 2,
            };
            var product7 = new Product
            {
                Id = 7,
                Name = "Hajcsat2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 2500,
                ProductCategoryId = 2,
            };
            var product8 = new Product
            {
                Id = 8,
                Name = "Hajcsat3",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 1500,
                ProductCategoryId = 2,
            };
            var product9 = new Product
            {
                Id = 9,
                Name = "Hajgumi1",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 2000,
                ProductCategoryId = 3,
            };
            var product10 = new Product
            {
                Id = 10,
                Name = "Hajgumi2",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                Price = 3000,
                ProductCategoryId = 3,
            };
            modelBuilder.Entity<Product>()
                .HasData(
                    product1,
                    product2,
                    product3,
                    product4,
                    product5,
                    product6,
                    product7,
                    product8,
                    product9,
                    product10
                );

            #endregion

        }
    }
}
