using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
  public interface IProductCategoryRepository
  {
    int AddProductCategory(ProductCategory productCategory);

    Task<IEnumerable<ProductCategory>> GetAllAsync();

    Task<ProductCategory> GetProductCategoryByIdAsync(int productCategoryId);

    void UpdateProductCategory(ProductCategory productCategory);

    Task DeleteProductCategoryAsync(int productCategoryId);
    
  }
}
