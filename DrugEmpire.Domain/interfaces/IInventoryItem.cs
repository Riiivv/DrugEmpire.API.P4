using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IInventoryItem
    {
        Task<List<InventoryItem>> GetAllInventoryItemsAsync();
        Task<InventoryItem> GetInventoryItemByIdAsync(int id);
        Task<InventoryItem> CreateInventoryItemAsync(InventoryItem inventoryItem);
        Task<InventoryItem> UpdateInventoryItemAsync(int id, InventoryItem updateInventoryItem);
        Task<bool> DeleteInventoryItemByIdAsync(int id);

    }
}
