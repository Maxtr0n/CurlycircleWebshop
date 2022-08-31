using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using DAL;
using Domain.Entities;
using Domain.Entities.QueryParameters;
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
        public Task<EntityCreatedViewModel> CreateProduct()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            double price = double.Parse(Request.Form["price"].First());
            string name = Request.Form["name"].First();
            string? description = Request.Form["description"].FirstOrDefault();
            int productCategoryId = int.Parse(Request.Form["productCategoryId"].First());
            IEnumerable<int> colorIds = ConvertToColorIds(Request.Form["colorIds"].FirstOrDefault());
            int patternId = 0;
            int.TryParse(Request.Form["patternId"].FirstOrDefault(), out patternId);
            int materialId = 0;
            int.TryParse(Request.Form["materialId"].FirstOrDefault(), out materialId);
            bool isAvailable = bool.Parse(Request.Form["isAvailable"].First());
            IFormFile? thumbnailImage = Request.Form.Files.Where(i => i.Name == "thumbnailImage").FirstOrDefault();
            IEnumerable<IFormFile> productImages = Request.Form.Files.Where(i => i.Name.Contains("productImage")).ToList();

            ProductUpsertDto productUpsertDto = new ProductUpsertDto()
            {
                Name = name,
                Price = price,
                ProductCategoryId = productCategoryId,
                Description = description,
                MaterialId = materialId != 0 ? materialId : null,
                ColorIds = colorIds,
                PatternId = patternId != 0 ? patternId : null,
                IsAvailable = isAvailable,
                ThumbnailImage = thumbnailImage,
                ProductImages = productImages
            };

            return _productService.CreateProductAsync(productUpsertDto);
        }

        [HttpGet]
        public Task<PagedProductsViewModel> GetProducts([FromQuery] ProductQueryParameters productQueryParameters)
        {
            return _productService.GetAllProductsAsync(productQueryParameters);
        }

        [HttpGet("{productId}")]
        public Task<ProductViewModel> GetProductById([FromRoute] int productId)
        {
            return _productService.FindProductByIdAsync(productId);
        }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public Task UpdateProduct([FromRoute] int productId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            double price = double.Parse(Request.Form["price"].First());
            string name = Request.Form["name"].First();
            string description = Request.Form["description"].First();
            int productCategoryId = int.Parse(Request.Form["productCategoryId"].First());
            IEnumerable<int> colorIds = ConvertToColorIds(Request.Form["colorIds"].First());
            int patternId = int.Parse(Request.Form["patternId"].First());
            int materialId = int.Parse(Request.Form["materialId"].First());
            bool isAvailable = bool.Parse(Request.Form["isAvailable"].First());
            IFormFile? thumbnailImage = Request.Form.Files.Where(i => i.Name == "thumbnailImage").FirstOrDefault();
            IEnumerable<IFormFile> productImages = Request.Form.Files.Where(i => i.Name.Contains("productImage")).ToList();

            ProductUpsertDto productUpsertDto = new ProductUpsertDto()
            {
                Name = name,
                Price = price,
                ProductCategoryId = productCategoryId,
                Description = description,
                MaterialId = materialId,
                ColorIds = colorIds,
                PatternId = patternId,
                IsAvailable = isAvailable,
                ThumbnailImage = thumbnailImage,
                ProductImages = productImages
            };

            return _productService.UpdateProductAsync(productId, productUpsertDto);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteProduct([FromRoute] int productId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _productService.DeleteProductAsync(productId);
        }

        private static IEnumerable<int> ConvertToColorIds(string? formString)
        {
            if (formString == null)
            {
                return Enumerable.Empty<int>();
            }

            string[] colorStrings = formString.Split(';');
            List<int> colorIds = new List<int>();
            foreach (string colorString in colorStrings)
            {
                if (int.TryParse(colorString, out int colorId))
                {
                    colorIds.Add(colorId);
                }
            }

            return colorIds;
        }
    }
}
