using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "abc123");
            users.Add(admin);

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

            modelBuilder.Entity<IdentityUserRole<int>>()
                .HasData(new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = 1
                });

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

            #region Orders

            var order1 = new Order
            {
                Id = 1,
                OrderDateTime = new DateTime(2021, 11,21,21,21,21),
                Name = "Kovács János",
                Email = "kovacs.janos@example.com",
                City = "Göd",
                ZipCode = 2131,
                Address = "Example utca 15.",
                PhoneNumber = "+36 30 111 1111",
                Note = "Nem kérek csípőset.",
                ShippingMethod = ShippingMethod.MagyarPostaPont,
            };
            var order2 = new Order
            {
                Id = 2,
                OrderDateTime = new DateTime(2021, 2, 1, 2, 21, 31),
                Name = "Kedves Béla",
                Email = "kedves.bela@example.com",
                City = "Budapest",
                ZipCode = 1037,
                Address = "Example utca 20.",
                PhoneNumber = "+36 30 333 3333",
                ShippingMethod = ShippingMethod.HomeDelivery,
            };
            var order3 = new Order
            {
                Id = 3,
                OrderDateTime = new DateTime(2020, 9, 21, 15, 12, 45),
                Name = "Asztalos András",
                Email = "asztalos.andras@example.com",
                City = "Szentendre",
                ZipCode = 2222,
                Address = "Example utca 15.",
                PhoneNumber = "+36 30 222 2222",
                ShippingMethod = ShippingMethod.Foxpost,
            };
            modelBuilder.Entity<Order>()
                .HasData(
                    order1,
                    order2,
                    order3
                );

            #endregion

            #region OrderItems

            var orderItem1 = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,

            };
            var orderItem2 = new OrderItem
            {
                Id = 2,
                OrderId = 2,
                ProductId = 2,
                Quantity = 1,
            }; 
            var orderItem3 = new OrderItem
            {
                Id = 3,
                OrderId = 2,
                ProductId = 7,
                Quantity = 2,
            };
            var orderItem4 = new OrderItem
            {
                Id = 4,
                OrderId = 3,
                ProductId = 10,
                Quantity = 3,
            };
            modelBuilder.Entity<OrderItem>()
                .HasData(
                    orderItem1,
                    orderItem2,
                    orderItem3,
                    orderItem4
                );

            #endregion

        }
    }
}
