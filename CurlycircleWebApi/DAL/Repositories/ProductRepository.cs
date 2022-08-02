using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            return product.Id;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products
                .Include(o => o.ProductCategory)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _dbContext.Products
                .Include(o => o.ProductCategory)
                .FirstOrDefaultAsync(o => o.Id == productId);

            if (product == null)
            {
                throw new EntityNotFoundException($"Product with id {productId} not found.");
            }

            return product;
        }

        public void UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
            }
        }
    }
}
