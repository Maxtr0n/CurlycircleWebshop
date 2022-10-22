using AutoMapper;
using BLL.Dtos;
using BLL.Helpers;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class ProductCategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;
        private readonly Mock<IProductCategoryRepository> _productCategoryRepositoryMock;
        private readonly Mock<IImageHelper> _imageHelperMock;
        private readonly ProductCategoryService _productCategoryService;

        public ProductCategoryServiceTests()
        {
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();
            _productCategoryRepositoryMock = new Mock<IProductCategoryRepository>();
            _imageHelperMock = new Mock<IImageHelper>();
            _productCategoryService = new ProductCategoryService(_productCategoryRepositoryMock.Object, _unitOfWorkStub.Object,
                _mapperStub.Object, _imageHelperMock.Object);
        }

        [Fact]
        public async Task CreateProductCategoryAsync_WithValidDto_CreatesProductCategory()
        {
            var thumbNailImageMock = new Mock<IFormFile>();
            thumbNailImageMock.Setup(t => t.Length).Returns(1);

            var dto = new ProductCategoryUpsertDto()
            {
                Name = "Test",
                Description = "Test",
                ThumbnailImage = thumbNailImageMock.Object,
            };

            _imageHelperMock.Setup(i => i.CreateThumbnailFile(dto.ThumbnailImage, It.IsAny<string>()).Result)
                .Returns("TestFilePath");
            _productCategoryRepositoryMock.Setup(p => p.AddProductCategory(It.IsAny<ProductCategory>()))
                .Returns(1);

            var result = await _productCategoryService.CreateProductCategoryAsync(dto);

            Assert.Equal(1, result.Id);
            _imageHelperMock.Verify(i => i.CreateThumbnailFile(dto.ThumbnailImage, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateProductCategoryAsync_WithoutThumbnail_CreatesProductCategory()
        {
            var dto = new ProductCategoryUpsertDto()
            {
                Name = "Test",
                Description = "Test",
            };

            _productCategoryRepositoryMock.Setup(p => p.AddProductCategory(It.IsAny<ProductCategory>()))
                .Returns(1);

            var result = await _productCategoryService.CreateProductCategoryAsync(dto);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetAllProductCategoriesAsync_ReturnsAllProductCategories()
        {
            var productCategoryList = new List<ProductCategory>()
            {
                new ProductCategory() { Id = 1, Name = "Test1" },
                new ProductCategory() { Id = 2, Name = "Test2" },
            };
            var productCategoryViewModelList = new List<ProductCategoryViewModel>()
            {
                new ProductCategoryViewModel() { Id = 1, Name = "Test1" },
                new ProductCategoryViewModel() { Id = 2, Name = "Test2" },
            };

            _productCategoryRepositoryMock.Setup(p => p.GetAllAsync().Result)
                .Returns(productCategoryList);
            _mapperStub.Setup(m => m.Map<ProductCategoriesViewModel>(productCategoryList))
                .Returns(
                new ProductCategoriesViewModel()
                {
                    ProductCategories = productCategoryViewModelList
                });

            var result = await _productCategoryService.GetAllProductCategoriesAsync();

            Assert.Equal(1, result.ProductCategories.First().Id);
            Assert.Equal("Test1", result.ProductCategories.First().Name);
            Assert.Equal(2, result.ProductCategories.Last().Id);
            Assert.Equal("Test2", result.ProductCategories.Last().Name);
        }

        [Fact]
        public async Task FindProductCategoryByIdAsync_WithValidId_ReturnsCorrectProductCategory()
        {
            var productCategory = new ProductCategory()
            {
                Id = 1,
                Name = "Test"
            };

            var vm = new ProductCategoryViewModel()
            {
                Id = 1,
                Name = "Test"
            };

            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(1).Result)
                .Returns(productCategory);
            _mapperStub.Setup(m => m.Map<ProductCategoryViewModel>(productCategory))
                .Returns(vm);

            var result = await _productCategoryService.FindProductCategoryByIdAsync(1);

            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public async Task FindProductCategoryByIdAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(2))
                .ThrowsAsync(new EntityNotFoundException("Test"));

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _productCategoryService.FindProductCategoryByIdAsync(2));
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_WithValidIdAndDto_UpdatesProductCategory()
        {
            var thumbNailImageMock = new Mock<IFormFile>();
            thumbNailImageMock.Setup(t => t.Length).Returns(1);

            var dto = new ProductCategoryUpsertDto()
            {
                Name = "Test",
                Description = "Test",
                ThumbnailImage = thumbNailImageMock.Object,
            };

            var pr = new ProductCategory()
            {
                Id = 1,
                Name = "BeforeUpdate",
                ThumbnailImageUrl = "Test"
            };

            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(1).Result)
                .Returns(pr);

            await _productCategoryService.UpdateProductCategoryAsync(1, dto);

            _imageHelperMock.Verify(i => i.CreateThumbnailFile(dto.ThumbnailImage, It.IsAny<string>()), Times.Once);
            _imageHelperMock.Verify(i => i.DeleteImageFile(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductCategoryAsync_WithValidId_DeletesCorrectProductCategory()
        {
            var pr = new ProductCategory()
            {
                Id = 1,
                Name = "BeforeUpdate",
                ThumbnailImageUrl = "Test"
            };

            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(1).Result)
                .Returns(pr);

            await _productCategoryService.DeleteProductCategoryAsync(1);

            _productCategoryRepositoryMock.Verify(p => p.GetProductCategoryByIdAsync(1), Times.Once);
            Assert.False(pr.IsAvailable);
        }

        [Fact]
        public async Task DeleteProductCategoryAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(2)).ThrowsAsync(new EntityNotFoundException("Test"));
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _productCategoryService.DeleteProductCategoryAsync(2));
        }

        [Fact]
        public async Task AddProductAsync_WithValidIdAndDto_AddsProduct()
        {
            var productCreateDto = new ProductUpsertDto()
            {
                Name = "Test",
                Price = 3000
            };

            var product = new Product()
            {
                Id = 1,
                Name = "Test",
                Price = 3000
            };

            var productCategory = new ProductCategory()
            {
                Id = 1,
                Name = "BeforeUpdate",
                ThumbnailImageUrl = "Test"
            };

            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(1).Result)
                .Returns(productCategory);
            _mapperStub.Setup(m => m.Map<Product>(productCreateDto))
                .Returns(product);

            var result = await _productCategoryService.AddProductAsync(1, productCreateDto);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddProductAsync_WithInvalidIdAndDto_AddsProduct()
        {
            var productCreateDto = new ProductUpsertDto()
            {
                Name = "Test",
                Price = 3000
            };

            _productCategoryRepositoryMock.Setup(p => p.GetProductCategoryByIdAsync(2))
                .ThrowsAsync(new EntityNotFoundException("Test"));

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _productCategoryService.AddProductAsync(2, productCreateDto));
        }
    }
}
