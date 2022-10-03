using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.QueryParameters;
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
        private readonly IColorRepository _colorRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly IPatternRepository _patternRepository;

        public ProductRepository(
            ApplicationDbContext dbContext,
            IColorRepository colorRepository,
            IPatternRepository patternRepository,
            IMaterialRepository materialRepository
            )
        {
            this._dbContext = dbContext;
            this._colorRepository = colorRepository;
            this._patternRepository = patternRepository;
            this._materialRepository = materialRepository;
        }

        public int AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            return product.Id;
        }

        public async Task<PagedList<Product>> GetAllAsync(ProductQueryParameters productQueryParameters)
        {
            if (!productQueryParameters.ValidPriceRange)
            {
                throw new BadParameterException("Max price of product cannot be less than min price of product.");
            }

            var products = _dbContext.Products
                .Include(p => p.Colors)
                .Include(p => p.Material)
                .Include(p => p.Pattern)
                .Where(p => p.Price >= productQueryParameters.MinPrice && p.Price <= productQueryParameters.MaxPrice && p.IsAvailable);

            var colors = await _colorRepository.GetColorsByIdsAsync(productQueryParameters.ColorIds);
            var materials = await _materialRepository.GetMaterialsByIdAsync(productQueryParameters.MaterialIds);
            var patterns = await _patternRepository.GetPatternsByIdAsync(productQueryParameters.PatternIds);

            SearchByProductCategoryId(ref products, productQueryParameters.ProductCategoryId);
            SearchByColor(ref products, colors);
            SearchByPattern(ref products, patterns);
            SearchByMaterial(ref products, materials);

            return await PagedList<Product>.CreateAsync(products, productQueryParameters.PageIndex, productQueryParameters.PageSize);
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

        private void SearchByProductCategoryId(ref IQueryable<Product> products, int? productCategoryId)
        {
            if (!products.Any() || productCategoryId == null)
                return;
            products = products.Where(p => p.ProductCategoryId == productCategoryId);
        }

        private void SearchByMaterial(ref IQueryable<Product> products, IEnumerable<Material>? materials)
        {
            if (!products.Any() || materials == null || !materials.Any())
                return;
            products = products.Where(p => p.Material != null && materials.Contains(p.Material));
        }

        private void SearchByPattern(ref IQueryable<Product> products, IEnumerable<Pattern>? patterns)
        {
            if (!products.Any() || patterns == null || !patterns.Any())
                return;
            products = products.Where(p => p.Pattern != null && patterns.Contains(p.Pattern));
        }

        private void SearchByColor(ref IQueryable<Product> products, IEnumerable<Color>? colors)
        {
            if (!products.Any() || colors == null || !colors.Any())
                return;

            // get those products, where the product has any of the selected colors
            products = products.Where(p => p.Colors.Any(c => colors.Contains(c)));
        }
    }
}
