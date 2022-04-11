using AutoMapper;
using BLL.Dtos;
using BLL.Interfaces;
using BLL.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(
            ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EntityCreatedViewModel> AddCartItemAsync(int cartId, CartItemUpsertDto cartItemCreateDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);
            await _cartRepository.AddCartItemAsync(cartId, cartItem);

            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(cartItem.Id);
        }

        public async Task ClearCartAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            cart.ClearCart();
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<EntityCreatedViewModel> CreateCartAsync()
        {
            Cart cart = new Cart();
            var id = _cartRepository.CreateCartAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return new EntityCreatedViewModel(id);
        }

        public async Task DeleteCartAsync(int cartId)
        {
            await _cartRepository.DeleteCartAsync(cartId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CartViewModel> FindCartByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            var cartViewModel = _mapper.Map<CartViewModel>(cart);
            return cartViewModel;
        }

        public async Task<CartItemsViewModel> GetAllCartItemsAsync(int cartId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            var cartItemsViewModel = _mapper.Map<CartItemsViewModel>(cart.CartItems);
            return cartItemsViewModel;
        }

        public async Task RemoveCartItemAsync(int cartId, int cartItemId)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            cart.RemoveCartItem(cartItemId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(int cartId, int cartItemId, int quantity)
        {
            var cart = await _cartRepository.GetCartByIdAsync(cartId);
            cart.UpdateQuantity(cartItemId, quantity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
