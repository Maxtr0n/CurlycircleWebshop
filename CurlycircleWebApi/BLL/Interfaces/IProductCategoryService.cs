using BLL.Dtos;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductCategoryService
    {
        Task<EntityCreatedViewModel> CreateProductCategoryAsync(ProductCategoryUpsertDto productCategoryCreatetDto);

        Task<ProductCategoriesViewModel> GetAllProductCategoriesAsync();

        Task<ProductCategoryViewModel> FindProductCategoryByIdAsync(int productCategoryId);

        Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryUpsertDto productCategoryUpdateDto);

        Task DeleteProductCategoryAsync(int productCategoryId);

        Task<ProductsViewModel> GetAllProductCategoryProductsAsync(int productCategoryId);

        Task<EntityCreatedViewModel> AddProductAsync(int productCategoryId, ProductUpsertDto productCreateDto);
    }
}
