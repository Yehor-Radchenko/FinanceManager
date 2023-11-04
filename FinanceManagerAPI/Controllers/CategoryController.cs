using FinanceManagerAPI.Data.Category;
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
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryUpdateDto>))]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(OperationCategory))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory([FromRoute] int Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryCreateDto category)
        {
            if (await _categoryService.Create(category))
                return Ok("Successfully created");
            else return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory([FromBody] CategoryUpdateDto category)
        {
            if(await _categoryService.Update(category))
                return Ok("Updated successfully.");
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if(await _categoryService.Delete(id))
                return Ok("Deleted successfully.");
            return BadRequest();
        }
    }
}
