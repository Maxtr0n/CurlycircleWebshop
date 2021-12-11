using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public ProductsController(IIdentityHelper identityHelper, IProductCategoryService productCategoryService, IProductService productService)
        {
            _identityHelper = identityHelper;
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> CreateProduct([FromBody] ProductUpsertDto productCreateDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _productService.CreateProductAsync(productCreateDto);
        }

        [HttpGet]
        public Task<ProductsViewModel> GetProducts()
        {
            return _productService.GetAllProductsAsync();
        }

        [HttpGet("{productId}")]
        public Task<ProductViewModel> GetProductById([FromRoute] int productId)
        {
            return _productService.FindProductByIdAsync(productId);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateProduct([FromRoute] int productId, [FromBody] ProductUpsertDto productUpdateDto)
        {
            return _productService.UpdateProductAsync(productId, productUpdateDto);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteProduct([FromRoute] int productId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _productService.DeleteProductAsync(productId);
        }
    }
}
