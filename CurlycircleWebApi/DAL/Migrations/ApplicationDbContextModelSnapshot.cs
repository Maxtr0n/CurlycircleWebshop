﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("cartitemseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("cartseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderitemseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("productcategoryseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("productseq")
                .IncrementsBy(10);

            modelBuilder.Entity("Domain.Entities.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "705b5027-49cc-4945-8800-379f0f77dff3",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "f918b4ad-eb63-4299-898d-86e99f068f99",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ee302849-cf37-4231-a0ad-46f779801fb9",
                            Email = "admin@admin.com",
                            EmailConfirmed = false,
                            FirstName = "Máté",
                            LastName = "Schütz",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEMbYRwInWv7WFvhAY2N0ZJyY0q9+AhiVdtNa+fQY/yA6D2nCAZzNb3ktBXpO6FNW7A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6a2d0820-1704-4e55-9e9c-c4db1a108ec2",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f8defb11-15e9-44f9-99cf-6375a4f031c2",
                            Email = "user@user.com",
                            EmailConfirmed = false,
                            FirstName = "Béla",
                            LastName = "Kovács",
                            LockoutEnabled = false,
                            NormalizedEmail = "USER@USER.COM",
                            NormalizedUserName = "USER",
                            PasswordHash = "AQAAAAEAACcQAAAAEIt2l0IpNMuPj2KDNEqSt5kogBQ95EBg9ZKR4kG4xBluhX/q0mz0PtirGtsSuivMZw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "33aa571a-021b-45bf-a773-7e2fbc4557b6",
                            TwoFactorEnabled = false,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "cartseq");

                    b.Property<int?>("ApplicationUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique()
                        .HasFilter("[ApplicationUserId] IS NOT NULL");

                    b.ToTable("Carts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApplicationUserId = 2
                        });
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq");

                    b.Property<int?>("ApplicationUserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "productseq");

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Material")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pattern")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Curly1",
                            Pattern = 4,
                            Price = 2500.0,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            Id = 2,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Curly2",
                            Pattern = 4,
                            Price = 3000.0,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            Id = 3,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Curly3",
                            Pattern = 4,
                            Price = 3000.0,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            Id = 4,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Curly4",
                            Pattern = 4,
                            Price = 3500.0,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            Id = 5,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Curly5",
                            Pattern = 4,
                            Price = 3500.0,
                            ProductCategoryId = 1
                        },
                        new
                        {
                            Id = 6,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Hajcsat1",
                            Pattern = 4,
                            Price = 4000.0,
                            ProductCategoryId = 2
                        },
                        new
                        {
                            Id = 7,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Hajcsat2",
                            Pattern = 4,
                            Price = 2500.0,
                            ProductCategoryId = 2
                        },
                        new
                        {
                            Id = 8,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Hajcsat3",
                            Pattern = 4,
                            Price = 1500.0,
                            ProductCategoryId = 2
                        },
                        new
                        {
                            Id = 9,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Hajgumi1",
                            Pattern = 4,
                            Price = 2000.0,
                            ProductCategoryId = 3
                        },
                        new
                        {
                            Id = 10,
                            Color = 8,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Material = 3,
                            Name = "Hajgumi2",
                            Pattern = 4,
                            Price = 3000.0,
                            ProductCategoryId = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "productcategoryseq");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum metus nisi, nec rutrum erat pretium vitae.",
                            Name = "Curly hajgöndörítők"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Donec tincidunt nunc ac sapien blandit pellentesque. Nulla tincidunt dui vitae nibh aliquet, et efficitur dui dignissim.",
                            Name = "Hajcsatok"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Vestibulum aliquam gravida dui, ut volutpat nisi semper quis. ",
                            Name = "Hajgumik"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.OwnsOne("Domain.Entities.UserAddress", "Address", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Line1");

                            b1.Property<string>("Line2")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Line2");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner("User")
                                .HasForeignKey("UserId");

                            b1.Navigation("User");

                            b1.HasData(
                                new
                                {
                                    UserId = 1,
                                    City = "Göd",
                                    Line1 = "Sajó utca 19.",
                                    ZipCode = "2131"
                                },
                                new
                                {
                                    UserId = 2,
                                    City = "Göd",
                                    Line1 = "Sajó utca 19.",
                                    Line2 = "Fsz.",
                                    ZipCode = "2131"
                                });
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Cart", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithOne("Cart")
                        .HasForeignKey("Domain.Entities.Cart", "ApplicationUserId");

                    b.OwnsMany("Domain.Entities.CartItem", "CartItems", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseHiLo(b1.Property<int>("Id"), "cartitemseq");

                            b1.Property<int>("CartId")
                                .HasColumnType("int");

                            b1.Property<double>("Price")
                                .HasColumnType("float");

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("CartId");

                            b1.HasIndex("ProductId");

                            b1.ToTable("CartItem");

                            b1.WithOwner("Cart")
                                .HasForeignKey("CartId");

                            b1.HasOne("Domain.Entities.Product", "Product")
                                .WithMany()
                                .HasForeignKey("ProductId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Cart");

                            b1.Navigation("Product");
                        });

                    b.Navigation("ApplicationUser");

                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", null)
                        .WithMany("Orders")
                        .HasForeignKey("ApplicationUserId");

                    b.OwnsOne("Domain.Entities.OrderAddress", "Address", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("City");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Line1");

                            b1.Property<string>("Line2")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Line2");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ZipCode");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner("Order")
                                .HasForeignKey("OrderId");

                            b1.Navigation("Order");
                        });

                    b.OwnsMany("Domain.Entities.OrderItem", "OrderItems", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseHiLo(b1.Property<int>("OrderId"), "orderitemseq");

                            b1.Property<double>("Price")
                                .HasColumnType("float");

                            b1.Property<int>("ProductId")
                                .HasColumnType("int");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("OrderId");

                            b1.HasIndex("ProductId");

                            b1.ToTable("OrderItem");

                            b1.WithOwner("Order")
                                .HasForeignKey("OrderId");

                            b1.HasOne("Domain.Entities.Product", "Product")
                                .WithMany()
                                .HasForeignKey("ProductId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Order");

                            b1.Navigation("Product");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
