using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface ICartItem
    {
        Task<CartItem> GetByCartIdAndProductIdAsync(int cartId, int productId);
        Task<List<CartItem>> GetAllCartItems();
        Task<CartItem> GetItemByIdAsync(int id);
        Task<CartItem> CreateCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(int id, CartItem cartItem);
        Task<bool> DeleteCartItemByIdAsync(int id);
    }
}
