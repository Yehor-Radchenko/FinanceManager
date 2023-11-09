using Azure;
using FinanceManagerAPI.Data.Category;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
using FinanceManagerAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagerAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FinanceManagerDbContext _context;
        public CategoryService(FinanceManagerDbContext context)
        {
            _context = context; 
        }

        public async Task<bool> Create(CategoryCreateDto model)
        {
            if (_context.Categories.Where(c => c.Name.Trim().ToUpper() == model.Name.Trim().ToUpper()).FirstOrDefault() is not null)
                throw new Exception("Category with this name is already exists.");
            try
            {
                OperationCategory category = new OperationCategory
                {
                    Name = model.Name,
                    Type = model.Type
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int? id)
        {
            if (!await _context.Categories.AnyAsync(c => c.Id == id))
                throw new Exception($"Сategory with Id: {id} was not found");
            try
            {
                OperationCategory course = new OperationCategory { Id = id.Value };
                _context.Entry(course).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<CategoryViewModel>?> GetAll()
        {
            List<OperationCategory> categoryModels = await _context.Categories.ToListAsync();

            List<CategoryViewModel> categories = await _context.Categories
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = (int)x.Type
                })
                .ToListAsync();

            return categories;
        }


        public async Task<CategoryViewModel?> GetById(int id)
        {
            OperationCategory? category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);

            
            if (category is null)
                throw new Exception($"There is no categories with this Id: {id}");

            return new CategoryViewModel { Id = category.Id, Name = category.Name, Type = (int)category.Type };
        }

        public async Task<bool> Update(CategoryUpdateDto expectedEntityValues)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(g => g.Id == expectedEntityValues.Id);

            if (existingCategory is null)
                throw new ArgumentException("Category with the specified ID does not exist.");

            var categoryWithSameName = await _context.Categories.Where(c =>
                    c.Name.Trim().ToUpper() == expectedEntityValues.Name.Trim().ToUpper()
                    && c.Id != expectedEntityValues.Id)
                    .FirstOrDefaultAsync();

            if (categoryWithSameName is not null)
                throw new Exception("Category with this name already exists.");
            try
            {
                _context.Entry(existingCategory).CurrentValues.SetValues(expectedEntityValues);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
