using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItem _CartItemRepository;
        private readonly IProduct _ProductRepository;
        private readonly ICart _CartRepository;
        public CartItemService(ICart cartRepository, ICartItem CartItemRepository, IProduct ProductRepository)
        {
            _CartRepository = cartRepository;
            _CartItemRepository = CartItemRepository;
            _ProductRepository = ProductRepository;
        }
        public async Task<IEnumerable<CartItemDTOResponse>> GetAllCartItems()
        {
            var cartItems = await _CartItemRepository.GetAllCartItems();

            return cartItems.Select(i => new CartItemDTOResponse
            {
                CartId = i.CartId,
                CartItemId = i.CartItemId,
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            });
        }
        public async Task<CartItemDTOResponse> GetCartItemById(int id)
        {
            var cartItem = await _CartItemRepository.GetItemByIdAsync(id);
            if (cartItem == null) return null;

            return new CartItemDTOResponse
            {
                CartId = cartItem.CartId,
                CartItemId = cartItem.CartItemId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,

            };
        }
        public async Task<CartItemDTOResponse> CreateCartItem(CartItemDTORequest cartItemDTORequest)
        {
            if (cartItemDTORequest == null)
                throw new ArgumentNullException(nameof(cartItemDTORequest));

            if (cartItemDTORequest.CartId <= 0)
                throw new Exception("CartId is required");

            if (cartItemDTORequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (cartItemDTORequest.Quantity <= 0)
                throw new Exception("Quantity must be greater than 0");

            var cart = await _CartRepository.GetCartByIdAsync(cartItemDTORequest.CartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var product = await _ProductRepository.GetProductByIdAsync(cartItemDTORequest.ProductId);
            if (product == null)
                throw new Exception("Product not found");

            // 🔥 Tjek om item allerede findes
            var existingItem = await _CartItemRepository
                .GetByCartIdAndProductIdAsync(cartItemDTORequest.CartId, cartItemDTORequest.ProductId);

            // 👉 Hvis findes → opdater quantity
            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDTORequest.Quantity;

                var updated = await _CartItemRepository.UpdateCartItemAsync(existingItem.CartItemId, existingItem);

                return new CartItemDTOResponse
                {
                    CartItemId = updated.CartItemId,
                    CartId = updated.CartId,
                    ProductId = updated.ProductId,
                    ProductName = product.Name,
                    Quantity = updated.Quantity,
                    UnitPrice = updated.UnitPrice
                };
            }

            // 👉 Ellers opret nyt item
            var cartItem = new CartItem
            {
                CartId = cartItemDTORequest.CartId,
                ProductId = cartItemDTORequest.ProductId,
                Quantity = cartItemDTORequest.Quantity,
                UnitPrice = product.Price
            };

            var created = await _CartItemRepository.CreateCartItemAsync(cartItem);

            return new CartItemDTOResponse
            {
                CartItemId = created.CartItemId,
                CartId = created.CartId,
                ProductId = created.ProductId,
                ProductName = product.Name,
                Quantity = created.Quantity,
                UnitPrice = created.UnitPrice
            };
        }
        public async Task<CartItemDTOResponse> UpdateCartItem(int id, CartItemDTORequest cartItemDTORequest)
        {
            if (cartItemDTORequest == null)
                throw new ArgumentException(nameof(cartItemDTORequest));

            if (cartItemDTORequest.CartId <= 0)
                throw new Exception("CartId can't be empty");

            if (cartItemDTORequest.ProductId <= 0)
                throw new Exception("Product can't be empty");
            if (cartItemDTORequest.Quantity <= 0)
                throw new Exception("Quantity is needed");

            var existingCartItem = await _CartItemRepository.GetItemByIdAsync(id);
            if (existingCartItem == null)
                throw new Exception("CartItems not found");

            //update fields
            existingCartItem.ProductId = cartItemDTORequest.ProductId;
            existingCartItem.Quantity = cartItemDTORequest.Quantity;

            var updateExistingCart = await _CartItemRepository.UpdateCartItemAsync(id, existingCartItem);

            return new CartItemDTOResponse
            {
                CartItemId = existingCartItem.CartItemId,
                ProductId = existingCartItem.ProductId,
                Quantity = existingCartItem.Quantity,
            };

        }
        public async Task<bool> DeleteCartItem(int id)
        {
            var existingCartitem = await _CartItemRepository.GetItemByIdAsync(id);
            if (existingCartitem == null)
                throw new Exception("CartItem not found");

            await _CartItemRepository.DeleteCartItemByIdAsync(id);
            return true;
        }
    }
}