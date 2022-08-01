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
        public Task<EntityCreatedViewModel> CreateProduct()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            double price = double.Parse(Request.Form["price"].First());
            string name = Request.Form["name"].First();
            string description = Request.Form["description"].First();
            int productCategoryId = int.Parse(Request.Form["productCategoryId"].First());
            IEnumerable<Color> colors = ConvertToColors(Request.Form["colors"].First());
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
                Description = description,
                Material = material,
                Colors = colors,
                Pattern = pattern,
                IsAvailable = isAvailable,
                ThumbnailImage = thumbnailImage,
                ProductImages = productImages
            };

            return _productService.CreateProductAsync(productUpsertDto);
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
        public Task UpdateProduct([FromRoute] int productId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            double price = double.Parse(Request.Form["price"].First());
            string name = Request.Form["name"].First();
            string description = Request.Form["description"].First();
            int productCategoryId = int.Parse(Request.Form["productCategoryId"].First());
            IEnumerable<Color> colors = ConvertToColors(Request.Form["colors"].First());
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
                Description = description,
                Material = material,
                Colors = colors,
                Pattern = pattern,
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

        private static IEnumerable<Color> ConvertToColors(string asd)
        {
            string[] colorStrings = asd.Split(',');
            List<Color> colors = new List<Color>();
            foreach (string colorString in colorStrings)
            {
                colors.Add((Color)Enum.Parse(typeof(Color), colorString));
            }

            return colors;
        }
    }
}
