using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
  public interface IProductRepository
  {
    int AddProduct(Product product);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> GetProductByIdAsync(int productId);

    void UpdateProduct(Product product);

    Task DeleteProductAsync(int productId);
  }
}
