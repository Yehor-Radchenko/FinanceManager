﻿using Azure;
using FinanceManagerAPI.Data;
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

        public async Task Create(CategoryDto model)
        {
            if (_context.Categories.Where(c => c.Name.Trim().ToUpper() == model.Name.TrimEnd().ToUpper()).FirstOrDefault() is not null)
            {
                throw new Exception("Category with this name is already exists.");
            }
            OperationCategory category = new OperationCategory
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            OperationCategory course = new OperationCategory { Id = id.Value };
            _context.Entry(course).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>?> GetAll()
        {
            List<CategoryDto> categories = new List<CategoryDto>();
            List<OperationCategory> categoryModels = await _context.Categories.ToListAsync();
            foreach(var i in categoryModels)
            {
                categories.Add(new CategoryDto 
                { 
                    Id = i.Id, 
                    Name = i.Name, 
                    Type = i.Type 
                });
            }
            return categories;
        }

        public async Task<CategoryDto?> GetById(int id)
        {
            OperationCategory? category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category is null)
                throw new Exception($"There is no categories with this Id: {id}");

            return new CategoryDto { Id = category.Id, Name = category.Name, Type = category.Type};
        }

        public async Task Update(CategoryDto expectedEntityValues)
        {
            var existingCategory = await _context.Categories.FindAsync(expectedEntityValues.Id);

            if (existingCategory is null)
            {
                throw new ArgumentException("Category with the specified ID does not exist.");
            }

            var categoryWithSameName = await _context.Categories.Where(c => 
                c.Name.Trim().ToUpper() == expectedEntityValues.Name.TrimEnd().ToUpper() 
                && c.Id != expectedEntityValues.Id)
                .FirstOrDefaultAsync();

            if (categoryWithSameName is not null)
            {
                throw new Exception("Category with this name already exists.");
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(expectedEntityValues);

            await _context.SaveChangesAsync();
        }
    }
}
