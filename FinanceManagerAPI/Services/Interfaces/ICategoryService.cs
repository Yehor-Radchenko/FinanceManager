using Azure;
using FinanceManagerAPI.Data.Category;
using FinanceManagerAPI.Data.Operation;
using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryUpdateDto>?> GetAll();
        public Task<CategoryUpdateDto?> GetById(int id);
        public Task<bool> Create(CategoryCreateDto model);
        public Task<bool> Update(CategoryUpdateDto expectedEntityValues);
        public Task<bool> Delete(int? id);
    }
}
