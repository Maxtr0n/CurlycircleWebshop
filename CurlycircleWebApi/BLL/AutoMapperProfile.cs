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
            CreateMap<IEnumerable<Product>, ProductsViewModel>()
                .ForMember(pvm => pvm.Products, options => options.MapFrom(x => x));

            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryUpsertDto, ProductCategory>();
            CreateMap<IEnumerable<ProductCategory>, ProductCategoriesViewModel>()
                .ForMember(pvm => pvm.ProductCategories, options => options.MapFrom(x => x));

            CreateMap<Order, OrderViewModel>()
                .IncludeMembers(o => o.Address)
                .ForMember(ovm => ovm.UserId, options => options.MapFrom(o => o.ApplicationUserId));

            CreateMap<OrderAddress, OrderViewModel>();

            CreateMap<OrderUpsertDto, Order>()
                .ForPath(x => x.Address.ZipCode, options => options.MapFrom(x => x.ZipCode))
                .ForPath(x => x.Address.City, options => options.MapFrom(x => x.City))
                .ForPath(x => x.Address.Line1, options => options.MapFrom(x => x.Line1))
                .ForPath(x => x.Address.Line2, options => options.MapFrom(x => x.Line2));

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

            CreateMap<ColorUpsertDto, Color>();
            CreateMap<PatternUpsertDto, Pattern>();
            CreateMap<MaterialUpsertDto, Material>();

            CreateMap<Color, ColorViewModel>();
            CreateMap<IEnumerable<Color>, ColorsViewModel>()
                 .ForMember(c => c.Colors, options => options.MapFrom(x => x));

            CreateMap<Pattern, PatternViewModel>();
            CreateMap<IEnumerable<Pattern>, PatternsViewModel>()
                 .ForMember(c => c.Patterns, options => options.MapFrom(x => x));

            CreateMap<Material, MaterialViewModel>();
            CreateMap<IEnumerable<Material>, MaterialsViewModel>()
                 .ForMember(c => c.Materials, options => options.MapFrom(x => x));
        }
    }
}