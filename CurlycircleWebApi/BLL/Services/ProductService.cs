using AutoMapper;
using BLL.Dtos;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.QueryParameters;
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
        private readonly IMaterialRepository _materialRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IPatternRepository _patternRepository;

        public ProductService(
            IProductRepository productRepository,
            IMaterialRepository materialRepository,
            IColorRepository colorRepository,
            IPatternRepository patternRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ImageHelper thumbnailImageHelper
            )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageHelper = thumbnailImageHelper;
            _materialRepository = materialRepository;
            _colorRepository = colorRepository;
            _patternRepository = patternRepository;
        }

        public async Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto)
        {
            Material? material = null;
            Pattern? pattern = null;
            IEnumerable<Domain.Entities.Color> colors;

            if (productUpsertDto.MaterialId != null)
            {
                material = await _materialRepository.GetMaterialByIdAsync(productUpsertDto.MaterialId ?? default);
            }

            if (productUpsertDto.PatternId != null)
            {
                pattern = await _patternRepository.GetPatternByIdAsync(productUpsertDto.PatternId ?? default);
            }

            colors = await _colorRepository.GetColorsByIdsAsync(productUpsertDto.ColorIds);

            var thumbnailImage = productUpsertDto.ThumbnailImage;

            string fileName = string.Empty;
            string imageNames = string.Empty;

            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                fileName = await _imageHelper.CreateThumbnailFile(thumbnailImage, pathToProductThumbnails);
            }

            if (productUpsertDto.ProductImages != null && productUpsertDto.ProductImages.Any())
            {
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
                Colors = colors,
                Pattern = pattern,
                Material = material,
                IsAvailable = productUpsertDto.IsAvailable
            };


            var id = _productRepository.AddProduct(product);
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task<PagedProductsViewModel> GetAllProductsAsync(ProductQueryParameters productQueryParameters)
        {
            var products = await _productRepository.GetAllAsync(productQueryParameters);
            var productsViewModel = _mapper.Map<PagedProductsViewModel>(products);
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
            Material? material = null;
            Pattern? pattern = null;
            IEnumerable<Domain.Entities.Color> colors;

            if (productUpdateDto.MaterialId != null)
            {
                material = await _materialRepository.GetMaterialByIdAsync(productUpdateDto.MaterialId ?? default);
            }

            if (productUpdateDto.PatternId != null)
            {
                pattern = await _patternRepository.GetPatternByIdAsync(productUpdateDto.PatternId ?? default);
            }

            colors = await _colorRepository.GetColorsByIdsAsync(productUpdateDto.ColorIds);

            var oldProduct = await _productRepository.GetProductByIdAsync(productId);

            oldProduct.Name = productUpdateDto.Name;
            oldProduct.Price = productUpdateDto.Price;
            oldProduct.Description = productUpdateDto.Description;
            oldProduct.Colors = colors;
            oldProduct.Material = material;
            oldProduct.Pattern = pattern;
            oldProduct.IsAvailable = productUpdateDto.IsAvailable;

            var thumbnailImage = productUpdateDto.ThumbnailImage;
            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                var thumbnailToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductThumbnails, oldProduct.ThumbnailImageUrl);
                _imageHelper.DeleteImageFile(thumbnailToDelete);


                string fileName = await _imageHelper.CreateThumbnailFile(thumbnailImage, pathToProductThumbnails);

                oldProduct.ThumbnailImageUrl = fileName;
            }

            var productImages = productUpdateDto.ProductImages;
            if (productImages != null && productImages.Any())
            {
                string[] imagePaths = oldProduct.ImageUrls.Split(';');
                foreach (var imagePath in imagePaths)
                {
                    var imageToDelete = Path.Combine(Directory.GetCurrentDirectory(), pathToProductImages, imagePath);
                    _imageHelper.DeleteImageFile(imageToDelete);
                }

                string imageNames = await _imageHelper.CreateImageFiles(productUpdateDto.ProductImages, pathToProductImages);

                oldProduct.ImageUrls = imageNames;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            product.IsAvailable = false;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
