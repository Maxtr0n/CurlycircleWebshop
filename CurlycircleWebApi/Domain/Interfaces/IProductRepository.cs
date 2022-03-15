using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
