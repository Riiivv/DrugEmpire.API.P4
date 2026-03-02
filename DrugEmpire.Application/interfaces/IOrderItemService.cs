using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTOResponse>> GetAllOrderItems();
        Task<OrderItemDTOResponse> GetOrderItemById(int id);
        Task<OrderItemDTOResponse> CreateOrderItem(OrderItemDTORequest orderItemDtoRequest);
        Task<OrderItemDTOResponse> UpdateOrderItem(int id, OrderItemDTORequest orderItemDtoRequest);
        Task<bool> DeleteOrderItem(int id);
    }
}
