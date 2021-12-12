using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dtos;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Entities.Abstractions;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EntityBase, EntityCreatedViewModel>();

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

            CreateMap<OrderItem, OrderItemViewModel>();
            CreateMap<OrderItemUpsertDto, OrderItem>();
            CreateMap<IEnumerable<OrderItem>, OrderItemsViewModel>()
                .ForMember(ovm => ovm.OrderItems, options => options.MapFrom(x => x));
        }
    }
}