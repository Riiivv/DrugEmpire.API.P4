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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTOResponse>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTOResponse>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<OrderDTOResponse>> CreateOrder(
            [FromBody] OrderDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _orderService.CreateOrder(request);
            if (created == null)
                return BadRequest("Failed to create order.");

            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = created.OrderId },
                created
            );
        }

        // PUT: api/Order/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderDTOResponse>> UpdateOrder(
            int id,
            [FromBody] OrderDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har OrderId, så håndhæv match
            if (request.OrderId > 0 && id != request.OrderId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _orderService.UpdateOrder(id, request);
            return Ok(updated);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrder(id);
            if (!deleted) return NotFound("Order not found");

            return NoContent();
        }
    }
}