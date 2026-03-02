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
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        // GET: api/OrderItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTOResponse>>> GetAllOrderItems()
        {
            var items = await _orderItemService.GetAllOrderItems();
            return Ok(items);
        }

        // GET: api/OrderItem/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemDTOResponse>> GetOrderItemById(int id)
        {
            var item = await _orderItemService.GetOrderItemById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/OrderItem
        [HttpPost]
        public async Task<ActionResult<OrderItemDTOResponse>> CreateOrderItem(
            [FromBody] OrderItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _orderItemService.CreateOrderItem(request);
            if (created == null)
                return BadRequest("Failed to create order item.");

            return CreatedAtAction(
                nameof(GetOrderItemById),
                new { id = created.OrderItemId },
                created
            );
        }

        // PUT: api/OrderItem/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderItemDTOResponse>> UpdateOrderItem(
            int id,
            [FromBody] OrderItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis DTO har OrderItemId, så håndhæv match
            if (request.OrderItemId > 0 && id != request.OrderItemId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _orderItemService.UpdateOrderItem(id, request);
            return Ok(updated);
        }

        // DELETE: api/OrderItem/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var deleted = await _orderItemService.DeleteOrderItem(id);
            if (!deleted) return NotFound("Order item not found");

            return NoContent();
        }
    }
}
