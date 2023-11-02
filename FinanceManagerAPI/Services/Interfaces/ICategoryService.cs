using Azure;
using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDto>?> GetAll();
        public Task<CategoryDto?> GetById(int id);
        public Task Create(CategoryDto model);
        public Task Update(CategoryDto expectedEntityValues);
        public Task Delete(int? id);
    }
}
