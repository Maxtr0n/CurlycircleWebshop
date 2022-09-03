using BLL.Dtos;
using BLL.ViewModels;
using Domain.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto);

        Task<PagedProductsViewModel> GetAllProductsAsync(ProductQueryParameters productQueryParameters);

        Task<ProductViewModel> FindProductByIdAsync(int productId);

        Task UpdateProductAsync(int productId, ProductUpsertDto productUpdateDto);

        Task DeleteProductAsync(int productId);
    }
}
