using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
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
        private const string pathToProductCategoryThumbnails = @"wwwroot\images\ProductCategoryImages\Thumbnails";
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageHelper _imageHelper;

        public ProductCategoryService(
          IProductCategoryRepository productCategoryRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper,
          ImageHelper imageHelper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageHelper = imageHelper;
        }

        public async Task<EntityCreatedViewModel> CreateProductCategoryAsync(ProductCategoryUpsertDto productCategoryCreateDto)
        {
            var file = productCategoryCreateDto.ThumbnailImage;

            string fileName = string.Empty;

            if (file != null && file.Length > 0)
            {
                fileName = await _imageHelper.CreateThumbnailFile(file, pathToProductCategoryThumbnails);
            }

            ProductCategory productCategory = new()
            {
                Name = productCategoryCreateDto.Name,
                Description = productCategoryCreateDto.Description,
                ThumbnailImageUrl = fileName
            };


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

        public async Task UpdateProductCategoryAsync(int productCategoryId, ProductCategoryUpsertDto productCategoryUpdateDto)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);

            productCategory.Name = productCategoryUpdateDto.Name;
            productCategory.Description = productCategoryUpdateDto.Description;
            var file = productCategoryUpdateDto.ThumbnailImage;

            if (file != null && file.Length > 0)
            {
                var imageToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductCategoryThumbnails, productCategory.ThumbnailImageUrl);
                _imageHelper.DeleteImageFile(imageToDelete);

                string fileName = await _imageHelper.CreateThumbnailFile(file, pathToProductCategoryThumbnails);
                productCategory.ThumbnailImageUrl = fileName;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductCategoryAsync(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryByIdAsync(productCategoryId);
            if (productCategory.ThumbnailImageUrl != String.Empty)
            {
                var imageToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductCategoryThumbnails, productCategory.ThumbnailImageUrl);
                _imageHelper.DeleteImageFile(imageToDelete);
            }

            await _productCategoryRepository.DeleteProductCategoryAsync(productCategoryId);
            await _unitOfWork.SaveChangesAsync();
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
