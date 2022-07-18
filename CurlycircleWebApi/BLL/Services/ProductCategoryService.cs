using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public ProductCategoryService(
          IProductCategoryRepository productCategoryRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> CreateProductCategoryAsync(ProductCategoryUpsertDto productCategoryCreateDto, IFormFile thumbnailImage)
        {
            var fileName = productCategoryCreateDto.Name.ToLower().Trim().Replace(" ", "_");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\ProductCategoryImages\Thumbnails", fileName);
            ProductCategory productCategory;

            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await thumbnailImage.CopyToAsync(fileStream);
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
    }
}
