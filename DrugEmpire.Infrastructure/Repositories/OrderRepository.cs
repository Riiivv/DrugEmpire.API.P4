using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class OrderRepository: IOrder
    {
        private readonly DatabaseContext _context;
        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrderAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            return order;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order> UpdateOrderAsync(int id, Order updateOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            existingOrder.UserId = updateOrder.OrderId;
            existingOrder.OrderNumber = updateOrder.OrderNumber;
            existingOrder.Total = updateOrder.Total;
            await _context.SaveChangesAsync();
            return existingOrder;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
