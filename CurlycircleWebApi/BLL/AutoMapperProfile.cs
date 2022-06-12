using AutoMapper;
using BLL.Dtos;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Entities.Abstractions;
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
                .IncludeMembers(u => u.Address);

            CreateMap<UserAddress, UserViewModel>();

            CreateMap<RegisterDto, ApplicationUser>()
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2))
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.Email, options => options.MapFrom(x => x.Email));

            CreateMap<Product, ProductViewModel>()
                .ForMember(pvm => pvm.ImageUrls, options => options.MapFrom(p => p.ImageUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()));
            CreateMap<ProductUpsertDto, Product>()
                .ForMember(p => p.ImageUrls, options => options.MapFrom(dto => string.Join(";", dto.ImageUrls)));
            CreateMap<IEnumerable<Product>, ProductsViewModel>()
                .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x));

            CreateMap<ProductCategory, ProductCategoryViewModel>()
                .ForMember(pvm => pvm.ImageUrls, options => options.MapFrom(p => p.ImageUrls.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()));
            CreateMap<ProductCategoryUpsertDto, ProductCategory>()
                .ForMember(p => p.ImageUrls, options => options.MapFrom(dto => string.Join(";", dto.ImageUrls)));
            CreateMap<IEnumerable<ProductCategory>, ProductCategoriesViewModel>()
                .ForMember(pvm => pvm.ProductCategories, options => options.MapFrom(x => x));

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderUpsertDto, Order>()
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2));


            CreateMap<IEnumerable<Order>, OrdersViewModel>()
                .ForMember(ovm => ovm.Orders, options => options.MapFrom(x => x));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(o => o.Product, options => options.MapFrom(x => x.Product));
            CreateMap<OrderItemUpsertDto, OrderItem>();
            CreateMap<IEnumerable<OrderItem>, OrderItemsViewModel>()
                .ForMember(ovm => ovm.OrderItems, options => options.MapFrom(x => x));

            CreateMap<Cart, CartViewModel>();

            CreateMap<CartItem, CartItemViewModel>();

            CreateMap<CartItemUpsertDto, CartItem>();

            CreateMap<CartItemViewModel, CartItemUpsertDto>();

            CreateMap<IEnumerable<CartItem>, CartItemsViewModel>()
                .ForMember(civm => civm.CartItems, options => options.MapFrom(x => x));

            CreateMap<ApplicationUser, UserDataViewModel>()
                .IncludeMembers(u => u.Address);

            CreateMap<UserAddress, UserDataViewModel>();
        }
    }
}