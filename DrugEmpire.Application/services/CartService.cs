using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class CartService : ICartService
    {
        private readonly ICart _CartRepository;
        public CartService(ICart CarttRepository)
        {
            _CartRepository = CarttRepository;
        }
        public async Task<IEnumerable<CartDTOResponse>> GetAllCarts()
        {
            var carts = await _CartRepository.GetAllCartsAsync();

            return carts.Select(c => new CartDTOResponse
            {
                CartId = c.CartId,
                UserId = c.UserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });
        }
        public async Task<CartDTOResponse> GetCartById(int id)
        {
            var carts = await _CartRepository.GetCartByIdAsync(id);
            if (carts == null)
                throw new Exception("Cart not found");

            return new CartDTOResponse
            {
                CartId = carts.CartId,
                UserId = carts.UserId,
                CreatedAt = carts.CreatedAt,
                UpdatedAt = carts.UpdatedAt

            };
        }
        public async Task<CartDTOResponse> CreateCart(CartDTORequest cartDTORequest)
        {
            if (cartDTORequest == null)
                throw new ArgumentNullException(nameof(cartDTORequest));

            // Valider UserId (int)
            if (cartDTORequest.UserId <= 0)
                throw new Exception("UserId is required");

            var cart = new Cart
            {
                UserId = cartDTORequest.UserId,
                CreatedAt = cartDTORequest.CreatedAt,
                UpdatedAt = cartDTORequest.UpdatedAt
            };

            var created = await _CartRepository.CreateCartAsync(cart);

            return new CartDTOResponse
            {
                CartId = created.CartId,
                UserId = created.UserId,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,
            };
        }
        public async Task<CartDTOResponse> UpdateCart(int id, CartDTORequest cartDTORequest)
        {
            if (cartDTORequest == null)
                throw new ArgumentNullException(nameof(cartDTORequest));

            if (cartDTORequest?.UserId <= 0)
                throw new Exception("UserId is required");

            var existingCart = await _CartRepository.GetCartByIdAsync(id);
            if (existingCart == null)
                throw new Exception("Cart not found");

            existingCart.UserId = cartDTORequest.UserId;
            existingCart.UpdatedAt = DateTime.UtcNow;

            var updatedCart = await _CartRepository.UpdateCartAsync(id, existingCart);

            return new CartDTOResponse
            {
                CartId = updatedCart.CartId,
                UserId = updatedCart.UserId,
                CreatedAt = updatedCart.CreatedAt,
                UpdatedAt = updatedCart.UpdatedAt,

            };
        }
        public async Task <bool> DeleteCart(int id)
        {
            var existingCart = await _CartRepository.GetCartByIdAsync(id);
            if (existingCart == null)
                throw new Exception("Cart not found");

            await _CartRepository.DeleteCartByIdAsync(id);
            return true;
        }

    }
}
