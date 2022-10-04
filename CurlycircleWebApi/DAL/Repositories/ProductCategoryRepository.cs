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
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProductCategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int AddProductCategory(ProductCategory productCategory)
        {
            dbContext.ProductCategories.Add(productCategory);
            return productCategory.Id;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            var productCategories = await dbContext.ProductCategories
                .Where(pc => pc.IsAvailable)
                .ToListAsync();
            return productCategories;
        }

        public async Task<ProductCategory> GetProductCategoryByIdAsync(int productCategoryId)
        {
            var productCategory = await dbContext.ProductCategories
                .Include(pc => pc.Products)
                .FirstOrDefaultAsync(p => p.Id == productCategoryId);

            if (productCategory == null)
            {
                throw new EntityNotFoundException($"ProductCategory with id {productCategoryId} not found.");
            }

            return productCategory;
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            dbContext.ProductCategories.Update(productCategory);
        }

        public async Task DeleteProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await dbContext.ProductCategories.FindAsync(productCategoryId);
            if (productCategory != null)
            {
                dbContext.ProductCategories.Remove(productCategory);
            }
        }
    }
}
