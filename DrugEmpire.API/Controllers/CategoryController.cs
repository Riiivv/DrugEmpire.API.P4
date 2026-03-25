using DrugEmpire.Application.DTOs;
using DrugEmpire.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DrugEmpire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTOResponse>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTOResponse>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                return Ok(category);
            }
            catch
            {
                return NotFound("Category not found.");
            }
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTOResponse>> CreateCategory([FromBody] CategoryDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var created = await _categoryService.CreateCategory(request);
                return CreatedAtAction(nameof(GetCategoryById), new { id = created.CategoryId }, created);
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

        // PUT: api/Category/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTOResponse>> UpdateCategory(int id, [FromBody] CategoryDTORequest request)
        {
            if (request == null)
                return BadRequest("Request body cannot be empty.");

            try
            {
                var updated = await _categoryService.UpdateCategory(id, request);
                return Ok(updated);
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

        // DELETE: api/Category/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var deleted = await _categoryService.DeleteCategory(id);
                if (!deleted)
                    return NotFound("Category not found.");

                return NoContent();
            }
            catch
            {
                return NotFound("Category not found.");
            }
        }
    }
}