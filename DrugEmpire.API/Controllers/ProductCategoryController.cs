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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTOResponse>>> GetAllProductCategories()
        {
            var items = await _productCategoryService.GetAllProductCategories();
            return Ok(items);
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductCategoryDTOResponse>> GetProductCategoryById(int id)
        {
            var item = await _productCategoryService.GetProductCategoryById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTOResponse>> CreateProductCategory([FromBody] ProductCategoryDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var created = await _productCategoryService.CreateProductCategory(request);
            if (created == null)
                return BadRequest("Failed to create product category relation.");

            return Ok(created);
        }

        // PUT: api/ProductCategory/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductCategoryDTOResponse>> UpdateProductCategory(int id, [FromBody] ProductCategoryDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            var updated = await _productCategoryService.UpdateProductCategory(id, request);
            return Ok(updated);
        }

        // DELETE: api/ProductCategory/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var deleted = await _productCategoryService.DeleteProductCategory(id);
            if (!deleted) return NotFound("Product category relation not found");

            return NoContent();
        }
    }
}