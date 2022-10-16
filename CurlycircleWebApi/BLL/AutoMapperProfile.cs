using AutoMapper;
using BLL.Dtos;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Entities.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EntityBase, EntityCreatedViewModel>();

            CreateMap<ApplicationUser, EntityCreatedViewModel>();

            CreateMap<ApplicationUser, UserViewModel>()
                .IncludeMembers(u => u.Address)
                .ForMember(uvm => uvm.Role, opt => opt.Ignore())
                .ForMember(uvm => uvm.AccessToken, opt => opt.Ignore());

            CreateMap<UserAddress, UserViewModel>()
               .ForMember(x => x.Id, options => options.Ignore())
               .ForMember(x => x.CartId, options => options.Ignore())
               .ForMember(x => x.Email, options => options.Ignore())
               .ForMember(x => x.FirstName, options => options.Ignore())
               .ForMember(x => x.LastName, options => options.Ignore())
               .ForMember(x => x.PhoneNumber, options => options.Ignore())
               .ForMember(x => x.Role, options => options.Ignore())
               .ForMember(x => x.AccessToken, options => options.Ignore())
               .ForMember(x => x.RefreshToken, options => options.Ignore());

            CreateMap<RegisterDto, ApplicationUser>()
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2))
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.Cart, options => options.Ignore())
                .ForMember(x => x.Orders, options => options.Ignore())
                .ForMember(x => x.RefreshToken, options => options.Ignore())
                .ForMember(x => x.RefreshTokenExpiryTime, options => options.Ignore())
                .ForMember(x => x.Id, options => options.Ignore())
                .ForMember(x => x.NormalizedUserName, options => options.Ignore())
                .ForMember(x => x.NormalizedEmail, options => options.Ignore())
                .ForMember(x => x.EmailConfirmed, options => options.Ignore())
                .ForMember(x => x.PasswordHash, options => options.Ignore())
                .ForMember(x => x.SecurityStamp, options => options.Ignore())
                .ForMember(x => x.ConcurrencyStamp, options => options.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, options => options.Ignore())
                .ForMember(x => x.TwoFactorEnabled, options => options.Ignore())
                .ForMember(x => x.LockoutEnd, options => options.Ignore())
                .ForMember(x => x.LockoutEnabled, options => options.Ignore())
                .ForMember(x => x.AccessFailedCount, options => options.Ignore());

            CreateMap<Product, ProductViewModel>()
                .ForMember(pvm => pvm.ImageUrls, options => options.MapFrom(p => p.ImageUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()));
            CreateMap<IEnumerable<Product>, ProductsViewModel>()
                .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x));

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryUpsertDto, ProductCategory>()
               .ForMember(x => x.ThumbnailImageUrl, options => options.Ignore())
               .ForMember(x => x.IsAvailable, options => options.Ignore())
               .ForMember(x => x.Products, options => options.Ignore())
               .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<IEnumerable<ProductCategory>, ProductCategoriesViewModel>()
                .ForMember(pvm => pvm.ProductCategories, options => options.MapFrom(x => x));

            CreateMap<Order, OrderViewModel>()
                .IncludeMembers(o => o.Address)
                .ForMember(ovm => ovm.UserId, options => options.MapFrom(o => o.ApplicationUserId));

            CreateMap<OrderAddress, OrderViewModel>()
               .ForMember(x => x.Id, options => options.Ignore())
               .ForMember(x => x.OrderDateTime, options => options.Ignore())
               .ForMember(x => x.UserId, options => options.Ignore())
               .ForMember(x => x.OrderItems, options => options.Ignore())
               .ForMember(x => x.FirstName, options => options.Ignore())
               .ForMember(x => x.LastName, options => options.Ignore())
               .ForMember(x => x.Email, options => options.Ignore())
               .ForMember(x => x.Total, options => options.Ignore())
               .ForMember(x => x.ShippingMethod, options => options.Ignore())
               .ForMember(x => x.PaymentMethod, options => options.Ignore())
               .ForMember(x => x.PhoneNumber, options => options.Ignore())
               .ForMember(x => x.Note, options => options.Ignore());

            CreateMap<OrderUpsertDto, Order>()
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2))
               .ForMember(x => x.OrderDateTime, options => options.Ignore())
               .ForMember(x => x.ApplicationUser, options => options.Ignore())
               .ForMember(x => x.OrderItems, options => options.Ignore())
               .ForMember(x => x.OrderNumber, options => options.Ignore())
               .ForMember(x => x.Total, options => options.Ignore())
               .ForMember(x => x.WebPayment, options => options.Ignore())
               .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<PagedList<Order>, PagedOrdersViewModel>()
               .ForMember(ovm => ovm.Orders, options => options.MapFrom(x => x))
               .ForMember(ovm => ovm.PageSize, options => options.MapFrom(x => x.PageSize))
               .ForMember(ovm => ovm.PageIndex, options => options.MapFrom(x => x.PageIndex))
               .ForMember(ovm => ovm.TotalCount, options => options.MapFrom(x => x.TotalCount))
               .ForMember(ovm => ovm.TotalPages, options => options.MapFrom(x => x.TotalPages))
               .ForMember(ovm => ovm.HasNextPage, options => options.MapFrom(x => x.HasNextPage))
               .ForMember(ovm => ovm.HasPreviousPage, options => options.MapFrom(x => x.HasPreviousPage));

            CreateMap<PagedList<Product>, PagedProductsViewModel>()
               .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x))
               .ForMember(pvm => pvm.PageSize, options => options.MapFrom(x => x.PageSize))
               .ForMember(pvm => pvm.PageIndex, options => options.MapFrom(x => x.PageIndex))
               .ForMember(pvm => pvm.TotalCount, options => options.MapFrom(x => x.TotalCount))
               .ForMember(pvm => pvm.TotalPages, options => options.MapFrom(x => x.TotalPages))
               .ForMember(pvm => pvm.HasNextPage, options => options.MapFrom(x => x.HasNextPage))
               .ForMember(pvm => pvm.HasPreviousPage, options => options.MapFrom(x => x.HasPreviousPage));

            CreateMap<IEnumerable<Order>, OrdersViewModel>()
                .ForMember(ovm => ovm.Orders, options => options.MapFrom(x => x));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(o => o.Product, options => options.MapFrom(x => x.Product));
            CreateMap<OrderItemUpsertDto, OrderItem>()
               .ForMember(x => x.Order, options => options.Ignore())
               .ForMember(x => x.Product, options => options.Ignore())
               .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<IEnumerable<OrderItem>, OrderItemsViewModel>()
                .ForMember(ovm => ovm.OrderItems, options => options.MapFrom(x => x));

            CreateMap<Cart, CartViewModel>();

            CreateMap<CartItem, CartItemViewModel>();

            CreateMap<CartItemUpsertDto, CartItem>()
               .ForMember(x => x.Cart, options => options.Ignore())
               .ForMember(x => x.CartId, options => options.Ignore())
               .ForMember(x => x.Product, options => options.Ignore())
               .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<CartItemViewModel, CartItemUpsertDto>();

            CreateMap<IEnumerable<CartItem>, CartItemsViewModel>()
                .ForMember(civm => civm.CartItems, options => options.MapFrom(x => x));

            CreateMap<ApplicationUser, UserDataViewModel>()
                .IncludeMembers(u => u.Address);

            CreateMap<UserAddress, UserDataViewModel>()
               .ForMember(x => x.Email, options => options.Ignore())
               .ForMember(x => x.FirstName, options => options.Ignore())
               .ForMember(x => x.LastName, options => options.Ignore())
               .ForMember(x => x.PhoneNumber, options => options.Ignore());

            CreateMap<ColorUpsertDto, Color>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Products, opt => opt.Ignore());
            CreateMap<PatternUpsertDto, Pattern>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<MaterialUpsertDto, Material>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<Color, ColorViewModel>();
            CreateMap<IEnumerable<Color>, ColorsViewModel>()
                 .ForMember(c => c.Colors, options => options.MapFrom(x => x));

            CreateMap<Pattern, PatternViewModel>();
            CreateMap<IEnumerable<Pattern>, PatternsViewModel>()
                 .ForMember(c => c.Patterns, options => options.MapFrom(x => x));

            CreateMap<Material, MaterialViewModel>();
            CreateMap<IEnumerable<Material>, MaterialsViewModel>()
                 .ForMember(c => c.Materials, options => options.MapFrom(x => x));

            CreateMap<WebPayment, WebPaymentResultViewModel>();
        }
    }
}