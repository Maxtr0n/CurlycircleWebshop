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

            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForMember(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForMember(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForMember(x => x.Address.Line2, options => options.MapFrom(x => x.Line2));

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductUpsertDto, Product>();
            CreateMap<IEnumerable<Product>, ProductsViewModel>()
                .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x));

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryUpsertDto, ProductCategory>();
            CreateMap<IEnumerable<ProductCategory>, ProductCategoriesViewModel>()
                .ForMember(pvm => pvm.ProductCategories, options => options.MapFrom(x => x));

            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderUpsertDto, Order>();
            CreateMap<IEnumerable<Order>, OrdersViewModel>()
                .ForMember(ovm => ovm.Orders, options => options.MapFrom(x => x));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(o => o.Product, options => options.MapFrom(x => x.Product));
            CreateMap<OrderItemUpsertDto, OrderItem>();
            CreateMap<IEnumerable<OrderItem>, OrderItemsViewModel>()
                .ForMember(ovm => ovm.OrderItems, options => options.MapFrom(x => x));

            CreateMap<Cart, CartViewModel>();

            CreateMap<CartItemUpsertDto, CartItem>();

            CreateMap<CartItemViewModel, CartItemUpsertDto>();

            CreateMap<IEnumerable<CartItem>, CartItemsViewModel>()
                .ForMember(civm => civm.CartItems, options => options.MapFrom(x => x));
        }
    }
}