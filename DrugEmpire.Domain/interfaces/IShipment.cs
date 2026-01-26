using DrugEmpire.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Domain.interfaces
{
    public interface IShipment
    {
        Task<List<Shipment>> GetShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(int id);
        Task<Shipment> CreateShipmentAsync(Shipment shipment);
        Task<Shipment> UpdateShipmentAsync(int id, Shipment shipment);
        Task<bool> DeleteShipmentAsync(int id);

    }
}
