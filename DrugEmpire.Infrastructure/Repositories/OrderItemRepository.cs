using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItem
    {
        private readonly DatabaseContext _context;
        public OrderItemRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return orderItems;
        }
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException("OrderItem not found");
            }
            return orderItem;
        }
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
        public async Task<OrderItem> UpdateOrderItemAsync(int id, OrderItem updateOrderItem)
        {
            var existingOrderItem = await _context.OrderItems.FindAsync(id);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException("OrderItem not found");
            }
            existingOrderItem.OrderId = updateOrderItem.OrderId;
            existingOrderItem.ProductId = updateOrderItem.ProductId;
            existingOrderItem.Quantity = updateOrderItem.Quantity;
            existingOrderItem.Price = updateOrderItem.Price;
            await _context.SaveChangesAsync();
            return existingOrderItem;
        }
        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException("OrderItem not found");
            }
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
