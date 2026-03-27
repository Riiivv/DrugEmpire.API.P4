using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTOResponse>> GetAllOrders();
        Task<OrderDTOResponse> GetOrderById(int id);
        Task<IEnumerable<OrderDTOResponse>> GetOrdersByUserId(int userId);
        Task<OrderDTOResponse> CreateOrder(OrderDTORequest orderDtoRequest);
        Task<OrderDTOResponse> UpdateOrder(int id, OrderDTORequest orderDtoRequest);
        Task<bool> DeleteOrder(int id);
        Task<OrderDTOResponse> Checkout(CheckoutDTORequest request);
    }
}
