using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IInventoryItemService
    {
        Task<IEnumerable<InventoryItemDTOResponse>> GetAllInventoryItems();
        Task<InventoryItemDTOResponse> GetInventoryItemById(int id);
        Task<InventoryItemDTOResponse> CreateInventoryItem(InventoryItemDTORequest inventoryItemDtoRequest);
        Task<InventoryItemDTOResponse> UpdateInventoryItem(int id, InventoryItemDTORequest inventoryItemDtoRequest);
        Task<bool> DeleteInventoryItem(int id);
    }
}
