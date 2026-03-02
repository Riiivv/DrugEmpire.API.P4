using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IInventoryItem _InventoryItemRepository;
        public InventoryItemService(IInventoryItem InventoryItemRepository)
        {
            _InventoryItemRepository = InventoryItemRepository;
        }
        public async Task<IEnumerable<InventoryItemDTOResponse>> GetAllInventoryItems()
        {
            var inventoryItems = await _InventoryItemRepository.GetAllInventoryItemsAsync();

            return inventoryItems.Select(item => new InventoryItemDTOResponse
            {
                InventoryItemId = item.InventoryItemId,
                ProductId = item.ProductId,
                QuantityOnHand = item.QuantityOnHand,
                ReservedQuantity = item.ReservedQuantity,
                UpdatedAt = item.UpdatedAt
            });
        }

        public async Task<InventoryItemDTOResponse> GetInventoryItemById(int id)
        {
            var inventoryItem = await _InventoryItemRepository.GetInventoryItemByIdAsync(id);
            if (inventoryItem == null)
                throw new Exception("Inventory item not found");

            return new InventoryItemDTOResponse
            {
                InventoryItemId = inventoryItem.InventoryItemId,
                ProductId = inventoryItem.ProductId,
                QuantityOnHand = inventoryItem.QuantityOnHand,
                ReservedQuantity = inventoryItem.ReservedQuantity,
                UpdatedAt = inventoryItem.UpdatedAt
            };
        }

        public async Task<InventoryItemDTOResponse> CreateInventoryItem(InventoryItemDTORequest inventoryItemDtoRequest)
        {
            if (inventoryItemDtoRequest == null)
                throw new ArgumentNullException(nameof(inventoryItemDtoRequest));

            if (inventoryItemDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (inventoryItemDtoRequest.QuantityOnHand < 0)
                throw new Exception("QuantityOnHand cannot be negative");

            if (inventoryItemDtoRequest.ReservedQuantity < 0)
                throw new Exception("ReservedQuantity cannot be negative");

            var entity = new InventoryItem
            {
                ProductId = inventoryItemDtoRequest.ProductId.Value,
                QuantityOnHand = inventoryItemDtoRequest.QuantityOnHand,
                ReservedQuantity = inventoryItemDtoRequest.ReservedQuantity,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _InventoryItemRepository.CreateInventoryItemAsync(entity);

            return new InventoryItemDTOResponse
            {
                InventoryItemId = created.InventoryItemId,
                ProductId = created.ProductId,
                QuantityOnHand = created.QuantityOnHand,
                ReservedQuantity = created.ReservedQuantity,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<InventoryItemDTOResponse> UpdateInventoryItem(int id, InventoryItemDTORequest inventoryItemDtoRequest)
        {
            if (inventoryItemDtoRequest == null)
                throw new ArgumentNullException(nameof(inventoryItemDtoRequest));

            if (inventoryItemDtoRequest.ProductId <= 0)
                throw new Exception("ProductId is required");

            if (inventoryItemDtoRequest.QuantityOnHand < 0)
                throw new Exception("QuantityOnHand cannot be negative");

            if (inventoryItemDtoRequest.ReservedQuantity < 0)
                throw new Exception("ReservedQuantity cannot be negative");

            var existing = await _InventoryItemRepository.GetInventoryItemByIdAsync(id);
            if (existing == null)
                throw new Exception("Inventory item not found");

            existing.ProductId = inventoryItemDtoRequest.ProductId.Value;
            existing.QuantityOnHand = inventoryItemDtoRequest.QuantityOnHand;
            existing.ReservedQuantity = inventoryItemDtoRequest.ReservedQuantity;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _InventoryItemRepository.UpdateInventoryItemAsync(id, existing);

            return new InventoryItemDTOResponse
            {
                InventoryItemId = updated.InventoryItemId,
                ProductId = updated.ProductId,
                QuantityOnHand = updated.QuantityOnHand,
                ReservedQuantity = updated.ReservedQuantity,
                UpdatedAt = updated.UpdatedAt
            };
        }

        public async Task<bool> DeleteInventoryItem(int id)
        {
            var existing = await _InventoryItemRepository.GetInventoryItemByIdAsync(id);
            if (existing == null)
                throw new Exception("Inventory item not found");

            await _InventoryItemRepository.DeleteInventoryItemByIdAsync(id);
            return true;
        }
    }
}