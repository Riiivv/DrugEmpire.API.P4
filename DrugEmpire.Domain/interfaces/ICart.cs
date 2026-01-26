using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface ICart
    {
        Task<List<Cart>> GetAllCartsAsync();
        Task<List<Cart>> GetCartByIdAsync(int id);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(int id, Cart updateCart);
        Task<bool> DeleteCartByIdAsync(int id);

    }
}
