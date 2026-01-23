using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Infrastructure.Repositories
{
    public class ShipmentRepository: IShipment
    {
        private readonly DatabaseContext _context;
        public ShipmentRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<List<Shipment>> GetShipmentsAsync()
        {
            return await _context.Shipment.ToListAsync();
        }
        public async Task<Shipment> GetShipmentByIdAsync(int id)
        {
            var existingShipment = await _context.Shipment.FindAsync(id);
            if (existingShipment == null)
            {
                throw new Exception("Shipment not found");
            }
            return existingShipment;
        }
        public async Task<Shipment> CreateShipmentAsync(Shipment shipment)
        {
            _context.Shipment.Add(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }
        public async Task<Shipment> UpdateShipmentAsync(int id, Shipment shipment)
        {
            var existingShipment = await _context.Shipment.FindAsync(id);
            if (existingShipment == null)
            {
                throw new Exception("Shipment not found");
            }
            existingShipment.ShipmentId = shipment.ShipmentId;
            existingShipment.OrderId = shipment.OrderId;
            existingShipment.Carrier = shipment.Carrier;
            existingShipment.TrackingNumber = shipment.TrackingNumber;
            existingShipment.Status = shipment.Status;
            existingShipment.ShippedAt = shipment.ShippedAt;
            existingShipment.DeliveredAt = shipment.DeliveredAt;
            await _context.SaveChangesAsync();
            return existingShipment;
        }
        public async Task<bool> DeleteShipmentAsync(int id)
        {
            var existingShipment = await _context.Shipment.FindAsync(id);
            if (existingShipment == null)
            {
                throw new Exception("Shipment not found");
            }
            _context.Shipment.Remove(existingShipment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
