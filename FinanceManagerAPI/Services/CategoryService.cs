using Azure;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.Services.Interfaces;
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

        public async Task Create(OperationCategory model)
        {
            if (_context.Categories.FirstOrDefault(c => c.Name == model.Name) is not null)
            {
                throw new Exception("Category with this name is already exists.");
            }
            _context.Categories.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            OperationCategory course = new OperationCategory { Id = id.Value };
            _context.Entry(course).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OperationCategory>?> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<OperationCategory?> GetById(int id)
        {
            OperationCategory? category = await _context.Categories
                .Include(c => c.FinancialOperations)
                .FirstOrDefaultAsync(p => p.Id == id);
            return category;
        }

        public async Task Update(OperationCategory expectedEntityValues)
        {
            var existingCategory = await _context.Categories.FindAsync(expectedEntityValues.Id);

            if (existingCategory is null)
            {
                throw new ArgumentException("Category with the specified ID does not exist.");
            }

            var categoryWithSameName = await _context.Categories.FirstOrDefaultAsync(c => c.Name == expectedEntityValues.Name && c.Id != expectedEntityValues.Id);

            if (categoryWithSameName is not null)
            {
                throw new Exception("Category with this name already exists.");
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(expectedEntityValues);

            await _context.SaveChangesAsync();
        }
    }
}
