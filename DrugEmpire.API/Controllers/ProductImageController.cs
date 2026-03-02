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
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // GET: api/ProductImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImageDTOResponse>>> GetAllProductImages()
        {
            var images = await _productImageService.GetAllProductImages();
            return Ok(images);
        }

        // GET: api/ProductImage/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductImageDTOResponse>> GetProductImageById(int id)
        {
            var image = await _productImageService.GetProductImageById(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        // POST: api/ProductImage
        [HttpPost]
        public async Task<ActionResult<ProductImageDTOResponse>> CreateProductImage([FromBody] ProductImageDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _productImageService.CreateProductImage(request);
            if (created == null)
                return BadRequest("Failed to create product image.");

            return CreatedAtAction(nameof(GetProductImageById), new { id = created.ProductImageId }, created);
        }

        // PUT: api/ProductImage/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductImageDTOResponse>> UpdateProductImage(int id, [FromBody] ProductImageDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            // Hvis din DTO har ProductImageId, så håndhæv match:
            if (request.ProductImageId > 0 && id != request.ProductImageId)
                return BadRequest("Route ID does not match request body ID.");

            var updated = await _productImageService.UpdateProductImage(id, request);
            return Ok(updated);
        }

        // DELETE: api/ProductImage/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            var deleted = await _productImageService.DeleteProductImage(id);
            if (!deleted) return NotFound("Product image not found");

            return NoContent();
        }
    }
}