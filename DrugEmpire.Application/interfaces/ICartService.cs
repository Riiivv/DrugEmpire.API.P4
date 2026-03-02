using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDTOResponse>> GetAllCarts();
        Task<CartDTOResponse> GetCartById (int Id);
        Task<CartDTOResponse> CreateCart(CartDTORequest request);
        Task<CartDTOResponse> UpdateCart(int id, CartDTORequest request);
        Task<bool> DeleteCart(int id);
    }
}
