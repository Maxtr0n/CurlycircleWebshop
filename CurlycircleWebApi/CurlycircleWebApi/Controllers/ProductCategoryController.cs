using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoryController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IIdentityHelper identityHelper, IProductCategoryService productCategoryService, IProductService productService)
        {
            _identityHelper = identityHelper;
            _productCategoryService = productCategoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> CreateProductCategory()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            string name = Request.Form["name"];
            string description = Request.Form["description"];
            IFormFile? thumbnailImage = Request.Form.Files.FirstOrDefault();
            ProductCategoryUpsertDto productCategoryCreateDto = new ProductCategoryUpsertDto()
            {
                Name = name,
                Description = description,
                ThumbnailImage = thumbnailImage
            };

            return _productCategoryService.CreateProductCategoryAsync(productCategoryCreateDto);
        }

        [HttpGet]
        public Task<ProductCategoriesViewModel> GetProductCategories()
        {
            return _productCategoryService.GetAllProductCategoriesAsync();
        }

        [HttpGet("{productCategoryId}")]
        public Task<ProductCategoryViewModel> GetProductCategoryById([FromRoute] int productCategoryId)
        {
            return _productCategoryService.FindProductCategoryByIdAsync(productCategoryId);
        }

        [HttpPut("{productCategoryId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateProductCategory([FromRoute] int productCategoryId)
        {
            string name = Request.Form["name"];
            string description = Request.Form["description"];
            IFormFile? thumbnailImage = Request.Form.Files.FirstOrDefault();
            ProductCategoryUpsertDto productCategoryUpdateDto = new ProductCategoryUpsertDto()
            {
                Name = name,
                Description = description,
                ThumbnailImage = thumbnailImage
            };

            return _productCategoryService.UpdateProductCategoryAsync(productCategoryId, productCategoryUpdateDto);
        }

        [HttpDelete("{productCategoryId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteProductCategory([FromRoute] int productCategoryId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _productCategoryService.DeleteProductCategoryAsync(productCategoryId);
        }

        [HttpPost("{productCategoryId}/products")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> AddProduct([FromRoute] int productCategoryId, [FromBody] ProductUpsertDto productCreateDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _productCategoryService.AddProductAsync(productCategoryId, productCreateDto);
        }

        [HttpGet("{productCategoryId}/products")]
        public Task<ProductsViewModel> GetProducts([FromRoute] int productCategoryId)
        {
            return _productCategoryService.GetAllProductCategoryProductsAsync(productCategoryId);
        }
    }
}
