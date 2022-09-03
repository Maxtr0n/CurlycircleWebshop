using Domain.Entities;
using Domain.QueryParameters;
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

        Task<PagedList<Product>> GetAllAsync(ProductQueryParameters productQueryParameters);

        Task<Product> GetProductByIdAsync(int productId);

        void UpdateProduct(Product product);

        Task DeleteProductAsync(int productId);
    }
}
