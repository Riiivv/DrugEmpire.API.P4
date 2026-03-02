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
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDTOResponse>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCarts();
            return Ok(carts);
        }

        // GET: api/Cart/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDTOResponse>> GetCartById(int id)
        {
            try
            {
                var cart = await _cartService.GetCartById(id);
                if (cart == null) return NotFound();
                return Ok(cart);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<ActionResult<CartDTOResponse>> CreateCart([FromBody] CartDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var created = await _cartService.CreateCart(request);
                if (created == null)
                    return BadRequest("Failed to create cart.");

                return CreatedAtAction(nameof(GetCartById), new { id = created.CartId }, created);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                // fx hvis service tjekker at User findes
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Cart/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CartDTOResponse>> UpdateCart(int id, [FromBody] CartDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har CartId, så check match:
            if (request.CartId > 0 && id != request.CartId)
                return BadRequest("Route ID does not match request body ID.");

            try
            {
                var updated = await _cartService.UpdateCart(id, request);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cart not found.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                var deleted = await _cartService.DeleteCart(id);
                if (!deleted) return NotFound("Cart not found");
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cart not found");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}