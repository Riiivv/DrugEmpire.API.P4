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
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemController : ControllerBase
    {
        private readonly IInventoryItemService _inventoryItemService;

        public InventoryItemController(IInventoryItemService inventoryItemService)
        {
            _inventoryItemService = inventoryItemService;
        }

        // GET: api/InventoryItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTOResponse>>> GetAllInventoryItems()
        {
            var items = await _inventoryItemService.GetAllInventoryItems();
            return Ok(items);
        }

        // GET: api/InventoryItem/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InventoryItemDTOResponse>> GetInventoryItemById(int id)
        {
            var item = await _inventoryItemService.GetInventoryItemById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/InventoryItem
        [HttpPost]
        public async Task<ActionResult<InventoryItemDTOResponse>> CreateInventoryItem(
            [FromBody] InventoryItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _inventoryItemService.CreateInventoryItem(request);
            if (created == null)
                return BadRequest("Failed to create inventory item.");

            return CreatedAtAction(
                nameof(GetInventoryItemById),
                new { id = created.InventoryItemId },
                created
            );
        }

        // PUT: api/InventoryItem/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<InventoryItemDTOResponse>> UpdateInventoryItem(
            int id,
            [FromBody] InventoryItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har InventoryItemId, så håndhæv match
            if (request.InventoryItemId > 0 && id != request.InventoryItemId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _inventoryItemService.UpdateInventoryItem(id, request);
            return Ok(updated);
        }

        // DELETE: api/InventoryItem/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var deleted = await _inventoryItemService.DeleteInventoryItem(id);
            if (!deleted) return NotFound("Inventory item not found");

            return NoContent();
        }
    }
}