using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class CartRepository : ICart
    {
        private readonly DatabaseContext _context;
        public CartRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts.ToListAsync();
        }
        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == id);
            if (existingCart == null)
            {
                throw new Exception("Cart not found");
            }
            return existingCart;
        }
        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task<Cart> UpdateCartAsync(int id, Cart updateCart)
        {
            var existingCart = await _context.Carts.FindAsync(id);
            if (existingCart == null)
            {
                throw new Exception("Cart not found");
            }
            existingCart.UserId = updateCart.UserId;
          //  existingCart.TotalAmount = updateCart.TotalAmount;
            await _context.SaveChangesAsync();
            return existingCart;
        }
        public async Task<bool> DeleteCartByIdAsync(int id)
        {
            var existingCart = await _context.Carts.FindAsync(id);
            if (existingCart == null)
            {
                throw new Exception("Cart not found");
            }
            _context.Carts.Remove(existingCart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
