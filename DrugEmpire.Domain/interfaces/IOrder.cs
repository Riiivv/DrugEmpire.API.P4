using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IOrder
    {
        Task<List<Order>> GetAllOrderAsync(int userid);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(int id, Order updateOrder);
        Task<bool> DeleteOrderAsync(int id);
    }
}
