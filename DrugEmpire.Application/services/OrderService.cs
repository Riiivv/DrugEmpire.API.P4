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
        private readonly IOrderItem _OrderItemRepository;
        private readonly ICart _CartRepository;
        private readonly ICartItem _CartItemRepository;
        public OrderService(IOrder orderRepository, IOrderItem orderItemRepository, ICart cartRepository, ICartItem cartItemRepository)
        {
            _OrderRepository = orderRepository;
            _OrderItemRepository = orderItemRepository;
            _CartRepository = cartRepository;
            _CartItemRepository = cartItemRepository;
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
        public async Task<IEnumerable<OrderDTOResponse>> GetOrdersByUserId(int userId)
        {
            var orders = await _OrderRepository.GetOrdersByUserIdAsync(userId);

            return orders.Select(o => new OrderDTOResponse
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderNumber = o.OrderNumber,
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
        public async Task<OrderDTOResponse> Checkout(CheckoutDTORequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.UserId <= 0)
                throw new Exception("UserId is required");

            if (request.CartId <= 0)
                throw new Exception("CartId is required");

            var cart = await _CartRepository.GetCartByIdAsync(request.CartId);
            if (cart == null)
                throw new Exception("Cart not found");

            var cartItems = await _CartItemRepository.GetAllCartItems();
            var itemsForCart = cartItems.Where(i => i.CartId == request.CartId).ToList();

            if (!itemsForCart.Any())
                throw new Exception("Cart is empty");

            var subtotal = itemsForCart.Sum(i => i.UnitPrice * i.Quantity);

            var order = new Order
            {
                UserId = request.UserId,
                OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(),
                Status = "Pending",
                Subtotal = subtotal,
                Total = subtotal,
                CreatedAt = DateTime.UtcNow,

                ShippingName = request.ShippingName,
                ShippingStreet = request.ShippingStreet,
                ShippingCity = request.ShippingCity,
                ShippingPostalCode = request.ShippingPostalCode,
                ShippingCountry = request.ShippingCountry,
                ShippingPhoneNumber = request.ShippingPhoneNumber
            };

            var createdOrder = await _OrderRepository.CreateOrderAsync(order);

            foreach (var item in itemsForCart)
            {
                var orderItem = new OrderItem
                {
                    OrderId = createdOrder.OrderId,
                    ProductId = item.ProductId,
                    ProductNameSnapshot = item.Product?.Name ?? "",
                    UnitPriceSnapshot = item.UnitPrice,
                    Quantity = item.Quantity,
                    Price = item.UnitPrice * item.Quantity
                };

                await _OrderItemRepository.CreateOrderItemAsync(orderItem);
            }
            return new OrderDTOResponse
            {
                OrderId = createdOrder.OrderId,
                UserId = createdOrder.UserId,
                OrderNumber = createdOrder.OrderNumber,
                Status = createdOrder.Status,
                Subtotal = createdOrder.Subtotal,
                Total = createdOrder.Total,
                CreatedAt = createdOrder.CreatedAt,
                ShippingName = createdOrder.ShippingName,
                ShippingStreet = createdOrder.ShippingStreet,
                ShippingCity = createdOrder.ShippingCity,
                ShippingPostalCode = createdOrder.ShippingPostalCode,
                ShippingCountry = createdOrder.ShippingCountry,
                ShippingPhoneNumber = createdOrder.ShippingPhoneNumber
            };
        }
    }
}