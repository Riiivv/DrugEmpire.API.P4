using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrugEmpire.Application.services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipment _ShipmentRepository;

        public ShipmentService(IShipment shipmentRepository)
        {
            _ShipmentRepository = shipmentRepository;
        }

        public async Task<IEnumerable<ShipmentDTOResponse>> GetAllShipments()
        {
            var shipments = await _ShipmentRepository.GetShipmentsAsync();

            return shipments.Select(s => new ShipmentDTOResponse
            {
                ShipmentId = s.ShipmentId,
                OrderId = s.OrderId,
                Carrier = s.Carrier,
                TrackingNumber = s.TrackingNumber,
                Status = s.Status,
                ShippedAt = s.ShippedAt,
                DeliveredAt = s.DeliveredAt
            });
        }

        public async Task<ShipmentDTOResponse> GetShipmentById(int id)
        {
            var shipment = await _ShipmentRepository.GetShipmentByIdAsync(id);
            if (shipment == null)
                throw new Exception("Shipment not found");

            return new ShipmentDTOResponse
            {
                ShipmentId = shipment.ShipmentId,
                OrderId = shipment.OrderId,
                Carrier = shipment.Carrier,
                TrackingNumber = shipment.TrackingNumber,
                Status = shipment.Status,
                ShippedAt = shipment.ShippedAt,
                DeliveredAt = shipment.DeliveredAt
            };
        }

        public async Task<ShipmentDTOResponse> CreateShipment(ShipmentDTORequest shipmentDtoRequest)
        {
            if (shipmentDtoRequest == null)
                throw new ArgumentNullException(nameof(shipmentDtoRequest));

            if (shipmentDtoRequest.OrderId <= 0)
                throw new Exception("OrderId is required");

            if (string.IsNullOrWhiteSpace(shipmentDtoRequest.Carrier))
                throw new Exception("Carrier is required");

            if (string.IsNullOrWhiteSpace(shipmentDtoRequest.Status))
                throw new Exception("Status is required");

            var entity = new Shipment
            {
                OrderId = shipmentDtoRequest.OrderId,
                Carrier = shipmentDtoRequest.Carrier,
                TrackingNumber = shipmentDtoRequest.TrackingNumber,
                Status = shipmentDtoRequest.Status,
                ShippedAt = shipmentDtoRequest.ShippedAt,
                DeliveredAt = shipmentDtoRequest.DeliveredAt
            };

            var created = await _ShipmentRepository.CreateShipmentAsync(entity);

            return new ShipmentDTOResponse
            {
                ShipmentId = created.ShipmentId,
                OrderId = created.OrderId,
                Carrier = created.Carrier,
                TrackingNumber = created.TrackingNumber,
                Status = created.Status,
                ShippedAt = created.ShippedAt,
                DeliveredAt = created.DeliveredAt
            };
        }

        public async Task<ShipmentDTOResponse> UpdateShipment(int id, ShipmentDTORequest shipmentDtoRequest)
        {
            if (shipmentDtoRequest == null)
                throw new ArgumentNullException(nameof(shipmentDtoRequest));

            if (shipmentDtoRequest.OrderId <= 0)
                throw new Exception("OrderId is required");

            if (string.IsNullOrWhiteSpace(shipmentDtoRequest.Carrier))
                throw new Exception("Carrier is required");

            if (string.IsNullOrWhiteSpace(shipmentDtoRequest.Status))
                throw new Exception("Status is required");

            var existing = await _ShipmentRepository.GetShipmentByIdAsync(id);
            if (existing == null)
                throw new Exception("Shipment not found");

            existing.OrderId = shipmentDtoRequest.OrderId;
            existing.Carrier = shipmentDtoRequest.Carrier;
            existing.TrackingNumber = shipmentDtoRequest.TrackingNumber;
            existing.Status = shipmentDtoRequest.Status;
            existing.ShippedAt = shipmentDtoRequest.ShippedAt;
            existing.DeliveredAt = shipmentDtoRequest.DeliveredAt;

            var updated = await _ShipmentRepository.UpdateShipmentAsync(id, existing);

            return new ShipmentDTOResponse
            {
                ShipmentId = updated.ShipmentId,
                OrderId = updated.OrderId,
                Carrier = updated.Carrier,
                TrackingNumber = updated.TrackingNumber,
                Status = updated.Status,
                ShippedAt = updated.ShippedAt,
                DeliveredAt = updated.DeliveredAt
            };
        }

        public async Task<bool> DeleteShipment(int id)
        {
            var existing = await _ShipmentRepository.GetShipmentByIdAsync(id);
            if (existing == null)
                throw new Exception("Shipment not found");

            await _ShipmentRepository.DeleteShipmentAsync(id);
            return true;
        }
    }
}