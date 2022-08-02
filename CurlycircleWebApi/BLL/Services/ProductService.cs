using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
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
    public class ProductService : IProductService
    {
        private const string pathToProductThumbnails = @"wwwroot\images\ProductImages\Thumbnails";
        private const string pathToProductImages = @"wwwroot\images\ProductImages\Originals";
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageHelper _imageHelper;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ImageHelper thumbnailImageHelper
            )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageHelper = thumbnailImageHelper;
        }

        public async Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto)
        {
            var thumbnailImage = productUpsertDto.ThumbnailImage;

            string fileName = string.Empty;
            string imageNames = string.Empty;

            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                fileName = await _imageHelper.CreateThumbnailFile(thumbnailImage, pathToProductThumbnails);
                imageNames = await _imageHelper.CreateImageFiles(productUpsertDto.ProductImages, pathToProductImages);
            }

            Product product = new()
            {
                Price = productUpsertDto.Price,
                Name = productUpsertDto.Name,
                ProductCategoryId = productUpsertDto.ProductCategoryId,
                Description = productUpsertDto.Description,
                ThumbnailImageUrl = fileName,
                ImageUrls = imageNames,
                Colors = productUpsertDto.Colors,
                Pattern = productUpsertDto.Pattern,
                Material = productUpsertDto.Material,
                IsAvailable = productUpsertDto.IsAvailable ?? true
            };


            var id = _productRepository.AddProduct(product);
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task<ProductsViewModel> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productsViewModel = _mapper.Map<ProductsViewModel>(products);
            return productsViewModel;
        }

        public async Task<ProductViewModel> FindProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }

        public async Task UpdateProductAsync(int productId, ProductUpsertDto productUpdateDto)
        {
            var oldProduct = await _productRepository.GetProductByIdAsync(productId);

            var updatedProduct = new Product()
            {
                Price = productUpdateDto.Price,
                Name = productUpdateDto.Name,
                ProductCategoryId = productUpdateDto.ProductCategoryId,
                Description = productUpdateDto.Description,
                Colors = productUpdateDto.Colors,
                Pattern = productUpdateDto.Pattern,
                Material = productUpdateDto.Material,
                IsAvailable = productUpdateDto.IsAvailable ?? true,

                ThumbnailImageUrl = oldProduct.ThumbnailImageUrl,
                ImageUrls = oldProduct.ImageUrls,
            };

            var thumbnailImage = productUpdateDto.ThumbnailImage;
            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                var thumbnailToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductThumbnails, oldProduct.ThumbnailImageUrl);
                _imageHelper.DeleteImageFile(thumbnailToDelete);

                string[] imagePaths = oldProduct.ImageUrls.Split(';');
                foreach (var imagePath in imagePaths)
                {
                    var imageToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductImages, imagePath);
                    _imageHelper.DeleteImageFile(imageToDelete);
                }
                string fileName = await _imageHelper.CreateThumbnailFile(thumbnailImage, pathToProductThumbnails);
                string imageNames = await _imageHelper.CreateImageFiles(productUpdateDto.ProductImages, pathToProductImages);

                updatedProduct.ThumbnailImageUrl = fileName;
                updatedProduct.ImageUrls = imageNames;
            }

            _productRepository.UpdateProduct(updatedProduct);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product.ThumbnailImageUrl != string.Empty)
            {
                var thumbnailToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductThumbnails, product.ThumbnailImageUrl);
                _imageHelper.DeleteImageFile(thumbnailToDelete);
            }

            string[] imagePaths = product.ImageUrls.Split(';');
            foreach (var imagePath in imagePaths)
            {
                var imageToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductImages, imagePath);
                _imageHelper.DeleteImageFile(imageToDelete);
            }

            await _productRepository.DeleteProductAsync(productId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
