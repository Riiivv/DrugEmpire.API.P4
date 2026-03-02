using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class OrderService : IOrderService
    {
        private readonly IOrder _OrderRepository;
        public OrderService(IOrder orderRepository)
        {
            _OrderRepository = orderRepository;
        }
        public async Task<IEnumerable<OrderDTOResponse>> GetAllOrders()
        {
            var orders = await _OrderRepository.GetAllOrderAsync();

            return orders.Select(o => new OrderDTOResponse
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                Status = o.Status,
                Subtotal = o.Subtotal,
                Total = o.Total,
                CreatedAt = o.CreatedAt
            });
        }

        public async Task<OrderDTOResponse> GetOrderById(int id)
        {
            var order = await _OrderRepository.GetOrderByIdAsync(id);
            if (order == null)
                throw new Exception("Order not found");

            return new OrderDTOResponse
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Status = order.Status,
                Subtotal = order.Subtotal,
                Total = order.Total,
                CreatedAt = order.CreatedAt
            };
        }

        public async Task<OrderDTOResponse> CreateOrder(OrderDTORequest orderDtoRequest)
        {
            if (orderDtoRequest == null)
                throw new ArgumentNullException(nameof(orderDtoRequest));

            if (orderDtoRequest.UserId <= 0)
                throw new Exception("UserId is required");

            var order = new Order
            {
                UserId = orderDtoRequest.UserId,
                Status = "Pending",
                Subtotal = orderDtoRequest.Subtotal,
                Total = orderDtoRequest.Total,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _OrderRepository.CreateOrderAsync(order);

            return new OrderDTOResponse
            {
                OrderId = created.OrderId,
                UserId = created.UserId,
                Status = created.Status,
                Subtotal = created.Subtotal,
                Total = created.Total,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<OrderDTOResponse> UpdateOrder(int id, OrderDTORequest orderDtoRequest)
        {
            if (orderDtoRequest == null)
                throw new ArgumentNullException(nameof(orderDtoRequest));

            var existing = await _OrderRepository.GetOrderByIdAsync(id);
            if (existing == null)
                throw new Exception("Order not found");

            existing.Status = orderDtoRequest.Status;
            existing.Subtotal = orderDtoRequest.Subtotal;
            existing.Total = orderDtoRequest.Total;

            var updated = await _OrderRepository.UpdateOrderAsync(id, existing);

            return new OrderDTOResponse
            {
                OrderId = updated.OrderId,
                UserId = updated.UserId,
                Status = updated.Status,
                Subtotal = updated.Subtotal,
                Total = updated.Total,
                CreatedAt = updated.CreatedAt
            };
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var existing = await _OrderRepository.GetOrderByIdAsync(id);
            if (existing == null)
                throw new Exception("Order not found");

            await _OrderRepository.DeleteOrderAsync(id);
            return true;
        }
    }
}