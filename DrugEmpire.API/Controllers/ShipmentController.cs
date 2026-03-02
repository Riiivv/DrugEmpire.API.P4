using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using DrugEmpire.Domain.entities;
using DrugEmpire.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugEmpire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        // GET: api/Shipment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentDTOResponse>>> GetAllShipments()
        {
            var shipments = await _shipmentService.GetAllShipments();
            return Ok(shipments);
        }

        // GET: api/Shipment/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ShipmentDTOResponse>> GetShipmentById(int id)
        {
            var shipment = await _shipmentService.GetShipmentById(id);
            if (shipment == null) return NotFound();
            return Ok(shipment);
        }

        // POST: api/Shipment
        [HttpPost]
        public async Task<ActionResult<ShipmentDTOResponse>> CreateShipment(
            [FromBody] ShipmentDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _shipmentService.CreateShipment(request);
            if (created == null)
                return BadRequest("Failed to create shipment.");

            return CreatedAtAction(
                nameof(GetShipmentById),
                new { id = created.ShipmentId },
                created
            );
        }

        // PUT: api/Shipment/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ShipmentDTOResponse>> UpdateShipment(
            int id,
            [FromBody] ShipmentDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis DTO har ShipmentId, så håndhæv match
            if (request.ShipmentId > 0 && id != request.ShipmentId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _shipmentService.UpdateShipment(id, request);
            return Ok(updated);
        }

        // DELETE: api/Shipment/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var deleted = await _shipmentService.DeleteShipment(id);
            if (!deleted) return NotFound("Shipment not found");

            return NoContent();
        }
    }
}