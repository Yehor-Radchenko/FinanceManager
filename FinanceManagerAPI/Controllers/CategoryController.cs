using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(OperationCategory))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(int Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryDto category)
        {
            await _categoryService.Create(category);
            return Ok("Successfully created");
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryDto category)
        {
            await _categoryService.Update(category);
            return Ok("Updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.Delete(id);
            return Ok("Deleted successfully.");
        }
    }
}
