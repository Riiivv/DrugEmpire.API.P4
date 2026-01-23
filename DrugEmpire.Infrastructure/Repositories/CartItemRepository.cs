using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItem
    {
        private readonly DatabaseContext _context;
        public CartItemRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<CartItem>> GetAllCartItems()
        {
            {
                return await _context.CartItems.ToListAsync();
            }
        }
        public async Task<CartItem>GetItemByIdAsync(int id)
        {
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync();
           
            if (existingCartItem == null)
            {
                throw new Exception("Your cart is empty");
            }
            return existingCartItem;
        }

        public async Task<CartItem> CreateCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItem(int id, CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems.FindAsync(id);
            if (existingCartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            existingCartItem.Quantity = cartItem.Quantity;
            existingCartItem.ProductId = cartItem.ProductId;
            existingCartItem.UnitPrice = cartItem.UnitPrice;
            existingCartItem.CartItemId = cartItem.CartItemId;
            await _context.SaveChangesAsync();
            return existingCartItem;
        }

        public async Task<bool> DeleteCartItemByIdAsync(int id)
        {
            var existingCartItem = await _context.CartItems.FindAsync(id);
            if (existingCartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            _context.Remove(existingCartItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}