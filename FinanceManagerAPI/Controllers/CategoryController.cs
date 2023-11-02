using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OperationCategory>))]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(OperationCategory))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody]OperationCategory category)
        {
            await _categoryService.Create(category);
            return Ok("Successfully created");
        }
    }
}
