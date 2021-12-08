using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.ViewModels;

namespace BLL.Interfaces
{
  public interface IProductService
  {
    Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto);

    Task<ProductsViewModel> GetAllProductsAsync();

    Task<ProductViewModel> FindProductByIdAsync(int productId);

    Task UpdateProductAsync(int productId, ProductUpsertDto productUpdateDto);

    Task DeleteProductAsync(int productId);
  }
}
