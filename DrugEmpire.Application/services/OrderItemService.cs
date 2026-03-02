using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItem _OrderItemRepository;

        public OrderItemService(IOrderItem orderItemRepository)
        {
            _OrderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItemDTOResponse>> GetAllOrderItems()
        {
            var items = await _OrderItemRepository.GetAllOrderItemsAsync();

            return items.Select(i => new OrderItemDTOResponse
            {
                OrderItemId = i.OrderItemId,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                ProductNameSnapshot = i.ProductNameSnapshot,
                UnitPriceSnapshot = i.UnitPriceSnapshot,
                Quantity = i.Quantity
            });
        }

        public async Task<OrderItemDTOResponse> GetOrderItemById(int id)
        {
            var item = await _OrderItemRepository.GetOrderItemByIdAsync(id);
            if (item == null)
                throw new Exception("Order item not found");

            return new OrderItemDTOResponse
            {
                OrderItemId = item.OrderItemId,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                ProductNameSnapshot = item.ProductNameSnapshot,
                UnitPriceSnapshot = item.UnitPriceSnapshot,
                Quantity = item.Quantity
            };
        }

        public async Task<OrderItemDTOResponse> CreateOrderItem(OrderItemDTORequest orderItemDtoRequest)
        {
            if (orderItemDtoRequest == null)
                throw new ArgumentNullException(nameof(orderItemDtoRequest));

            if (orderItemDtoRequest.OrderId <= 0)
                throw new Exception("OrderId is required");

            if (orderItemDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (orderItemDtoRequest.Quantity <= 0)
                throw new Exception("Quantity must be greater than 0");

            // Opret entity (snapshots bør sættes ved create)
            var entity = new OrderItem
            {
                OrderId = orderItemDtoRequest.OrderId,
                ProductId = orderItemDtoRequest.ProductId,
                Quantity = orderItemDtoRequest.Quantity,

                // Hvis du har snapshot-felter i requesten, så brug dem:
                ProductNameSnapshot = orderItemDtoRequest.ProductNameSnapshot,
                UnitPriceSnapshot = orderItemDtoRequest.UnitPriceSnapshot
            };

            var created = await _OrderItemRepository.CreateOrderItemAsync(entity);

            return new OrderItemDTOResponse
            {
                OrderItemId = created.OrderItemId,
                OrderId = created.OrderId,
                ProductId = created.ProductId,
                ProductNameSnapshot = created.ProductNameSnapshot,
                UnitPriceSnapshot = created.UnitPriceSnapshot,
                Quantity = created.Quantity
            };
        }

        public async Task<OrderItemDTOResponse> UpdateOrderItem(int id, OrderItemDTORequest orderItemDtoRequest)
        {
            if (orderItemDtoRequest == null)
                throw new ArgumentNullException(nameof(orderItemDtoRequest));

            if (orderItemDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (orderItemDtoRequest.Quantity <= 0)
                throw new Exception("Quantity must be greater than 0");

            var existing = await _OrderItemRepository.GetOrderItemByIdAsync(id);
            if (existing == null)
                throw new Exception("Order item not found");

            // Whitelist: typisk ændrer du quantity (og evt. product hvis du tillader det)
            existing.ProductId = orderItemDtoRequest.ProductId;
            existing.Quantity = orderItemDtoRequest.Quantity;

            // Snapshots: normalt ændrer man dem ikke efter create,
            // men hvis du vil tillade det, kan du sætte dem her:
            existing.ProductNameSnapshot = orderItemDtoRequest.ProductNameSnapshot;
            existing.UnitPriceSnapshot = orderItemDtoRequest.UnitPriceSnapshot;

            var updated = await _OrderItemRepository.UpdateOrderItemAsync(id, existing);

            return new OrderItemDTOResponse
            {
                OrderItemId = updated.OrderItemId,
                OrderId = updated.OrderId,
                ProductId = updated.ProductId,
                ProductNameSnapshot = updated.ProductNameSnapshot,
                UnitPriceSnapshot = updated.UnitPriceSnapshot,
                Quantity = updated.Quantity
            };
        }

        public async Task<bool> DeleteOrderItem(int id)
        {
            var existing = await _OrderItemRepository.GetOrderItemByIdAsync(id);
            if (existing == null)
                throw new Exception("Order item not found");

            await _OrderItemRepository.DeleteOrderItemAsync(id);
            return true;
        }
    }
}