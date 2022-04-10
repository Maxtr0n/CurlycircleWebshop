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
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2))
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.Email, options => options.MapFrom(x => x.Email));

            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductUpsertDto, Product>();
            CreateMap<IEnumerable<Product>, ProductsViewModel>()
                .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x));

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryUpsertDto, ProductCategory>();
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

            CreateMap<CartItemUpsertDto, CartItem>();

            CreateMap<CartItemViewModel, CartItemUpsertDto>();

            CreateMap<IEnumerable<CartItem>, CartItemsViewModel>()
                .ForMember(civm => civm.CartItems, options => options.MapFrom(x => x));
        }
    }
}