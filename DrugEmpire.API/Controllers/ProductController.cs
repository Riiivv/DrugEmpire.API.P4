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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTOResponse>>> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTOResponse>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductDTOResponse>> CreateProduct([FromBody] ProductDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _productService.CreateProduct(request);
            if (created == null)
                return BadRequest("Failed to create product.");

            return CreatedAtAction(nameof(GetProductById), new { id = created.ProductId }, created);
        }

        // PUT: api/Product/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTOResponse>> UpdateProduct(int id, [FromBody] ProductDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har ProductId, så håndhæv match:
            if (request.ProductId > 0 && id != request.ProductId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _productService.UpdateProduct(id, request);
            return Ok(updated);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            if (!deleted) return NotFound("Product not found");

            return NoContent();
        }
    }
}