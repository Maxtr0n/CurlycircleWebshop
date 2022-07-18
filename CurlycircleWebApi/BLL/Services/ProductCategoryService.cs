using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly long _fileSizeLimit;

        public ProductCategoryService(
          IProductCategoryRepository productCategoryRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper,
          IConfiguration configuration)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileSizeLimit = configuration.GetValue<long>("ImageSizeLimit");
        }

        public async Task<EntityCreatedViewModel> CreateProductCategoryAsync(ProductCategoryUpsertDto productCategoryCreateDto)
        {
            var file = productCategoryCreateDto.ThumbnailImage;
            ProductCategory productCategory;

            if (file != null && file.Length > 0)
            {
                CheckFileExtension(file);
                CheckFileSize(file);

                var fileName = productCategoryCreateDto.Name.ToLower().Trim().Replace(" ", "_") + Path.GetExtension(file.FileName).ToLowerInvariant();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\ProductCategoryImages\Thumbnails", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                productCategory = new ProductCategory
                {
                    Name = productCategoryCreateDto.Name,
                    Description = productCategoryCreateDto.Description,
                    ThumbnailImageUrl = fileName
                };
            }
            else
            {
                productCategory = new ProductCategory
                {
                    Name = productCategoryCreateDto.Name,
                    Description = productCategoryCreateDto.Description
                };
            }

            var id = _productCategoryRepository.AddProductCategory(productCategory);
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task<ProductCategoriesViewModel> GetAllProductCategoriesAsync()
        {
            var productCategories = await _productCategoryRepository.GetAllAsync();
            var productCategoriesViewModel = _mapper.Map<ProductCategoriesViewModel>(productCategories);
            return productCategoriesViewModel;
        }

        public async Task<ProductCategoryViewModel> FindProductCategoryByIdAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
            var productCategoryViewModel = _mapper.Map<ProductCategoryViewModel>(productCategory);
            return productCategoryViewModel;
        }

        public Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryUpsertDto productCategoryUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductCategoryAsync(int productCategoryId)
        {
            await _productCategoryRepository.DeleteProductCategoryAsync(productCategoryId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductsViewModel> GetAllProductCategoryProductsAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
            var productsViewModel = _mapper.Map<ProductsViewModel>(productCategory.Products);
            return productsViewModel;
        }

        public async Task<EntityCreatedViewModel> AddProductAsync(int productCategoryId, ProductUpsertDto productCreateDto)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
            var product = _mapper.Map<Product>(productCreateDto);
            productCategory.AddProduct(product);

            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(product.Id);
        }

        private void CheckFileExtension(IFormFile file)
        {
            string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                throw new ValidationAppException("File upload failed.", new[]
                {
                    "The extension of the file is not accepted."
                });
            }
        }

        private void CheckFileSize(IFormFile file)
        {
            if (file.Length > _fileSizeLimit)
            {
                throw new ValidationAppException("File upload failed.", new[]
            {
                    "The size of the file is too big."
                });
            }
        }
    }
}
