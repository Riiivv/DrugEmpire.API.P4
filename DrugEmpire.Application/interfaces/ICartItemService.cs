using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDTOResponse>> GetAllCartItems();
        Task<CartItemDTOResponse> GetCartItemById(int id);
        Task<CartItemDTOResponse> CreateCartItem(CartItemDTORequest cartItemDtoRequest);
        Task<CartItemDTOResponse> UpdateCartItem(int id, CartItemDTORequest cartItemDtoRequest);
        Task<bool> DeleteCartItem(int id);
    }
}
