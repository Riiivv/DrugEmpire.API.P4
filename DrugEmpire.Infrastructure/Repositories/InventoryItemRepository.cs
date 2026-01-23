using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class InventoryItemRepository : IInventoryItem
    {
        private readonly DatabaseContext _context;
        public InventoryItemRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<InventoryItem>> GetAllInventoryItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }
        public async Task<InventoryItem> GetInventoryItemByIdAsync(int id)
        {
            var existingInventoryItem = await _context.InventoryItems.FindAsync(id);
            if (existingInventoryItem == null)
            {
                throw new Exception("Inventory item not found");
            }
            return existingInventoryItem;
        }
        public async Task<InventoryItem> CreateInventoryItemAsync(InventoryItem inventoryItem)
        {
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();
            return inventoryItem;
        }
        public async Task<InventoryItem> UpdateInventoryItemAsync(int id, InventoryItem updateInventoryItem)
        {
            var existingInventoryItem = await _context.InventoryItems.FindAsync(id);
            if (existingInventoryItem == null)
            {
                throw new Exception("Inventory item not found");
            }
            existingInventoryItem.ProductId = updateInventoryItem.ProductId;
            existingInventoryItem.QuantityOnHand = updateInventoryItem.ReservedQuantity;
            existingInventoryItem.ReservedQuantity = updateInventoryItem.ReservedQuantity;
            await _context.SaveChangesAsync();
            return existingInventoryItem;
        }
        public async Task<bool> DeleteInventoryItemByIdAsync(int id)
        {
            var existingInventoryItem = await _context.InventoryItems.FindAsync(id);
            if (existingInventoryItem == null)
            {
                throw new Exception("Inventory item not found");
            }
            _context.InventoryItems.Remove(existingInventoryItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
