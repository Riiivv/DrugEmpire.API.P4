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
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        // GET: api/CartItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTOResponse>>> GetAllCartItems()
        {
            var items = await _cartItemService.GetAllCartItems();
            return Ok(items);
        }

        // GET: api/CartItem/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDTOResponse>> GetCartItemById(int id)
        {
            var item = await _cartItemService.GetCartItemById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/CartItem
        [HttpPost]
        public async Task<ActionResult<CartItemDTOResponse>> CreateCartItem([FromBody] CartItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _cartItemService.CreateCartItem(request);
            if (created == null)
                return BadRequest("Failed to create cart item.");

            return CreatedAtAction(nameof(GetCartItemById), new { id = created.CartItemId }, created);
        }

        // PUT: api/CartItem/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CartItemDTOResponse>> UpdateCartItem(int id, [FromBody] CartItemDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har CartItemId, så håndhæv match:
            if (request.CartItemId > 0 && id != request.CartItemId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _cartItemService.UpdateCartItem(id, request);
            return Ok(updated);
        }

        // DELETE: api/CartItem/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var deleted = await _cartItemService.DeleteCartItem(id);
            if (!deleted) return NotFound("Cart item not found");

            return NoContent();
        }
    }
}