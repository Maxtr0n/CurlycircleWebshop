using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using DAL;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public ProductController(IIdentityHelper identityHelper, IProductCategoryService productCategoryService, IProductService productService)
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
            double price = double.Parse(Request.Form["price"].First());
            string name = Request.Form["name"].First();
            int productCategoryId = int.Parse(Request.Form["productCategoryId"].First());
            Color color = (Color)Enum.Parse(typeof(Color), Request.Form["color"].First());
            Pattern pattern = (Pattern)Enum.Parse(typeof(Color), Request.Form["pattern"].First()); ;
            Material material = (Material)Enum.Parse(typeof(Color), Request.Form["material"].First()); ;
            bool isAvailable = bool.Parse(Request.Form["isAvailable"].First());
            IFormFile? thumbnailImage = Request.Form.Files.Where(i => i.Name == "thumbnailImage").FirstOrDefault();
            IEnumerable<IFormFile> productImages = Request.Form.Files.Where(i => i.Name.Contains("productImage")).ToList();

            ProductUpsertDto productUpsertDto = new ProductUpsertDto()
            {
                Name = name,
                Price = price,
                ProductCategoryId = productCategoryId,
                Description = productCreateDto.Description,
                Material = material,
                Color = color,
                Pattern = pattern,
                IsAvailable = isAvailable,
                ThumbnailImage = thumbnailImage,
                ProductImages = productImages
            };

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
