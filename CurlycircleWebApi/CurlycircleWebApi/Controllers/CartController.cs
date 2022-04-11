using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using CurlycircleWebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurlycircleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ApiController
    {
        private readonly IIdentityHelper _identityHelper;
        private readonly ICartService _cartService;

        public CartController(IIdentityHelper identityHelper, ICartService cartService)
        {
            _identityHelper = identityHelper;
            _cartService = cartService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> CreateCart()
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _cartService.CreateCartAsync();
        }

        [HttpPost("{cartId}/cartItems")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public Task<EntityCreatedViewModel> AddCartItem([FromRoute] int cartId, [FromBody] CartItemUpsertDto cartItemUpsertDto)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status201Created;
            return _cartService.AddCartItemAsync(cartId, cartItemUpsertDto);
        }

        [HttpDelete("{cartId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task DeleteCart([FromRoute] int cartId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _cartService.DeleteCartAsync(cartId);
        }

        [HttpDelete("{cartId}/clear")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task ClearCart([FromRoute] int cartId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _cartService.ClearCartAsync(cartId);
        }

        [HttpDelete("{cartId}/cartItems/{cartItemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public Task ClearCart([FromRoute] int cartId, [FromRoute] int cartItemId)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return _cartService.RemoveCartItemAsync(cartId, cartItemId);
        }

        [HttpGet("{cartId}")]
        public Task<CartViewModel> GetCartById([FromRoute] int cartId)
        {
            return _cartService.FindCartByIdAsync(cartId);
        }

        [HttpGet("{cartId}/cartItems")]
        public Task<CartItemsViewModel> GetCartItems([FromRoute] int cartId)
        {
            return _cartService.GetAllCartItemsAsync(cartId);
        }

        [HttpPut("{cartId}/cartItems/{cartItemId}")]
        public Task UpdateCartItem([FromRoute] int cartId, [FromRoute] int cartItemId, [FromBody] int quantity)
        {
            return _cartService.UpdateCartItemAsync(cartId, cartItemId, quantity);
        }

    }
}
