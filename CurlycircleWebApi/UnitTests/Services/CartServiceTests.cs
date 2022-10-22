using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.Services;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkStub;
        private readonly Mock<IMapper> _mapperStub;

        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _unitOfWorkStub = new Mock<IUnitOfWork>();
            _mapperStub = new Mock<IMapper>();

            _cartService = new CartService(_cartRepositoryMock.Object, _unitOfWorkStub.Object, _mapperStub.Object);
        }

        [Fact]
        public async Task CreateCartAsync_CreatesNewCartAndReturnsId()
        {
            var cart = new Cart()
            {
                Id = 1
            };

            _cartRepositoryMock.Setup(c => c.CreateCart(It.IsAny<Cart>()))
                .Returns(1);

            var result = await _cartService.CreateCartAsync();

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteCartAsync_WithValidId_DeletesCart()
        {
            await _cartService.DeleteCartAsync(1);

            _cartRepositoryMock.Verify(c => c.DeleteCartAsync(1), Times.Once);
        }

        [Fact]
        public async Task FindCartByIdAsync_WithValidId_ReturnsCart()
        {
            var cart = new Cart()
            {
                Id = 1
            };

            var vm = new CartViewModel()
            {
                Id = 1
            };

            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(cart);
            _mapperStub.Setup(m => m.Map<CartViewModel>(cart))
                .Returns(vm);

            var result = await _cartService.FindCartByIdAsync(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task FindCartByIdAsync_WithInvalidId_ThrowsEntityNotFoundException()
        {
            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1))
                 .ThrowsAsync(new EntityNotFoundException("Test"));

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _cartService.FindCartByIdAsync(1));
        }

        [Fact]
        public async Task GetAllCartItemsAsync_WithValidCartId_ReturnsCartItems()
        {
            var cart = new Cart()
            {
                Id = 1
            };

            var vm = new CartItemsViewModel()
            {
                CartItems = new List<CartItemViewModel>()
                {
                    new CartItemViewModel()
                    {
                        Id = 1,
                        CartId = 1
                    }
                }
            };

            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(cart);
            _mapperStub.Setup(m => m.Map<CartItemsViewModel>(cart.CartItems))
                .Returns(vm);

            var result = await _cartService.GetAllCartItemsAsync(1);

            Assert.Equal(vm, result);
            Assert.Equal(vm.CartItems.ToList()[0], result.CartItems.ToList()[0]);
        }

        [Fact]
        public async Task RemoveCartItemAsync_WithValidIds_RemovesCartItem()
        {
            var cart = new Cart()
            {
                Id = 1,
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        Id = 1
                    }
                }
            };

            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(cart);

            await _cartService.RemoveCartItemAsync(1, 1);

            Assert.Empty(cart.CartItems);
        }

        [Fact]
        public async Task UpdateCartItemAsync_WithValidIds_UpdatesCartItem()
        {
            var cart = new Cart()
            {
                Id = 1,
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        Id = 1
                    }
                }
            };

            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(cart);

            await _cartService.UpdateCartItemAsync(1, 1, 2);

            Assert.NotEmpty(cart.CartItems);
            Assert.Single(cart.CartItems);
            Assert.Equal(2, cart.CartItems.ToList()[0].Quantity);
        }

        [Fact]
        public async Task ClearCartAsync_WithValidIds_ClearsCart()
        {
            var cart = new Cart()
            {
                Id = 1,
                CartItems = new List<CartItem>()
                {
                    new CartItem()
                    {
                        Id = 1
                    }
                }
            };

            _cartRepositoryMock.Setup(c => c.GetCartByIdAsync(1).Result)
                .Returns(cart);

            await _cartService.ClearCartAsync(1);

            Assert.Empty(cart.CartItems);
        }

        [Fact]
        public async Task AddCartItemAsync_WithValidIds_AddsCartItem()
        {
            var cart = new Cart()
            {
                Id = 1,
                CartItems = new List<CartItem>()
            };

            var dto = new CartItemUpsertDto()
            {
                Quantity = 1,
                Price = 1000,
                ProductId = 1
            };

            var cartItem = new CartItem()
            {
                Id = 1,
                ProductId = 1,
                Price = 1000,
                Quantity = 1,
                CartId = 1
            };

            _mapperStub.Setup(m => m.Map<CartItem>(dto))
                .Returns(cartItem);
            _cartRepositoryMock.Setup(c => c.AddCartItemAsync(1, cartItem))
                .Callback(() => cart.AddCartItem(cartItem));

            await _cartService.AddCartItemAsync(1, dto);

            Assert.NotEmpty(cart.CartItems);
            Assert.Single(cart.CartItems.ToList());
            Assert.Equal(1, cart.CartItems.ToList()[0].Id);
        }
    }
}
