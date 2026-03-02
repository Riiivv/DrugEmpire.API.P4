using DrugEmpire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDTOResponse>> GetAllShipments();
        Task<ShipmentDTOResponse> GetShipmentById(int id);
        Task<ShipmentDTOResponse> CreateShipment(ShipmentDTORequest shipmentDtoRequest);
        Task<ShipmentDTOResponse> UpdateShipment(int id, ShipmentDTORequest shipmentDtoRequest);
        Task<bool> DeleteShipment(int id);
    }
}
