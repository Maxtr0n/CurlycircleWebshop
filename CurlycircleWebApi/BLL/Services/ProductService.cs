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
    public class ProductService : IProductService
    {
        private const string pathToProductThumbnails = @"wwwroot\images\ProductImages\Thumbnails";
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ThumbnailImageHelper _thumbnailImageHelper;

        public ProductService(
          IProductRepository productRepository,
          IUnitOfWork unitOfWork,
          IMapper mapper,
           ThumbnailImageHelper thumbnailImageHelper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _thumbnailImageHelper = thumbnailImageHelper;
        }

        public async Task<EntityCreatedViewModel> CreateProductAsync(ProductUpsertDto productUpsertDto)
        {
            var thumbnailImage = productUpsertDto.ThumbnailImage;
            Product product;

            if (thumbnailImage != null && thumbnailImage.Length > 0)
            {
                string fileName = await _thumbnailImageHelper.CreateThumbnailFile(thumbnailImage, pathToProductThumbnails);
            }

            var product = _mapper.Map<Product>(productUpsertDto);

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
            var product = await _productRepository.GetProductByIdAsync(productId);
            product.Update(_mapper.Map<Product>(productUpdateDto));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
