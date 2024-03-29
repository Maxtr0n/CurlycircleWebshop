﻿using AutoMapper;
using BLL.Dtos;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.QueryParameters;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMaterialRepository> _materialRepositoryMock;
        private readonly Mock<IPatternRepository> _patternRepositoryMock;
        private readonly Mock<IColorRepository> _colorRepositoryMock;
        private readonly Mock<IImageHelper> _imageHelperStub;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _materialRepositoryMock = new Mock<IMaterialRepository>();
            _patternRepositoryMock = new Mock<IPatternRepository>();
            _colorRepositoryMock = new Mock<IColorRepository>();
            _imageHelperStub = new Mock<IImageHelper>();
            _productService = new ProductService(_productRepositoryMock.Object, _materialRepositoryMock.Object,
                _colorRepositoryMock.Object, _patternRepositoryMock.Object, _unitOfWorkStub.Object,
                _mapperStub.Object, _imageHelperStub.Object);
        }

        [Fact]
        public async Task CreateProductAsync_WithValidDto_CreatesProduct()
        {
            var dto = new ProductUpsertDto()
            {
                Name = "Test",
                Price = 3000
            };

            var material = new Material()
            {
                Id = 1,
            };

            var pattern = new Pattern()
            {
                Id = 1,
            };

            var colorList = new List<Color>()
            {
                new Color() { Id = 1, Name = "Test" },
            };


            _materialRepositoryMock.Setup(m => m.GetMaterialByIdAsync(1).Result)
                .Returns(material);
            _patternRepositoryMock.Setup(m => m.GetPatternByIdAsync(1).Result)
                .Returns(pattern);
            _colorRepositoryMock.Setup(m => m.GetColorsByIdsAsync(new List<int>() { 1 }).Result)
                .Returns(colorList);
            _productRepositoryMock.Setup(p => p.AddProduct(It.IsAny<Product>()))
                .Returns(1);

            var result = await _productService.CreateProductAsync(dto);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            var productQueryParameters = new ProductQueryParameters()
            {

            };

            var products = new List<Product>()
            {
                new Product()
            };

            var pagedProducts = new PagedList<Product>(products, 1, 1, 1);

            var vm = new PagedProductsViewModel()
            {
                Products = new List<ProductViewModel>()
                {
                    new ProductViewModel()
                },
                TotalPages = 1,
                TotalCount = 1,
                PageIndex = 1,
                PageSize = 1,
            };

            _productRepositoryMock.Setup(p => p.GetAllAsync(productQueryParameters).Result)
                .Returns(pagedProducts);
            _mapperStub.Setup(m => m.Map<PagedProductsViewModel>(pagedProducts))
                .Returns(vm);

            var result = await _productService.GetAllProductsAsync(productQueryParameters);

            Assert.Single(result.Products.ToList());
            Assert.Equal(1, result.TotalCount);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(1, result.PageSize);
            Assert.Equal(1, result.PageIndex);
        }

        [Fact]
        public async Task FindProductByIdAsync_WithValidId_ReturnsProduct()
        {
            var product = new Product()
            {
                Id = 1,
            };

            var vm = new ProductViewModel()
            {
                Id = 1
            };

            _productRepositoryMock.Setup(p => p.GetProductByIdAsync(1).Result)
                .Returns(product);
            _mapperStub.Setup(m => m.Map<ProductViewModel>(product))
                .Returns(vm);

            var res = await _productService.FindProductByIdAsync(product.Id);

            Assert.Equal(1, res.Id);
        }

        [Fact]
        public async Task FindProductByIdAsync_WithInalidId_ThrowsEntityNotFoundException()
        {
            _productRepositoryMock.Setup(p => p.GetProductByIdAsync(1))
                .ThrowsAsync(new EntityNotFoundException("Test"));

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _productService.FindProductByIdAsync(1));
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidIdAndDto_UpdatesProduct()
        {
            var dto = new ProductUpsertDto()
            {
                Name = "Test",
                Price = 3000,
                MaterialId = 1,
                PatternId = 1,
                ColorIds = new List<int>() { 1 }
            };

            var material = new Material()
            {
                Id = 1,
            };

            var pattern = new Pattern()
            {
                Id = 1,
            };

            var colorList = new List<Color>()
            {
                new Color() { Id = 1, Name = "Test" },
            };


            _materialRepositoryMock.Setup(m => m.GetMaterialByIdAsync(1).Result)
                .Returns(material);
            _patternRepositoryMock.Setup(m => m.GetPatternByIdAsync(1).Result)
                .Returns(pattern);
            _colorRepositoryMock.Setup(m => m.GetColorsByIdsAsync(new List<int>() { 1 }).Result)
                .Returns(colorList);
            _productRepositoryMock.Setup(p => p.GetProductByIdAsync(1).Result)
                .Returns(new Product());

            await _productService.UpdateProductAsync(1, dto);

            _materialRepositoryMock.Verify(m => m.GetMaterialByIdAsync(1), Times.Once);
            _patternRepositoryMock.Verify(m => m.GetPatternByIdAsync(1), Times.Once);
            _colorRepositoryMock.Verify(m => m.GetColorsByIdsAsync(It.IsAny<List<int>>()), Times.Once);
            _productRepositoryMock.Verify(p => p.GetProductByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_WithValidId_DeletesProduct()
        {
            var product = new Product()
            {
                Id = 1,
                IsAvailable = true
            };

            _productRepositoryMock.Setup(p => p.GetProductByIdAsync(1).Result)
                .Returns(product);

            await _productService.DeleteProductAsync(1);

            _productRepositoryMock.Verify(p => p.GetProductByIdAsync(1), Times.Once);
            Assert.False(product.IsAvailable);
        }
    }
}
