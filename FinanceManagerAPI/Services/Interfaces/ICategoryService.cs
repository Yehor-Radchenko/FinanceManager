using Azure;
using FinanceManagerAPI.Data.Category;
using FinanceManagerAPI.Data.Operation;
using FinanceManagerAPI.Models;
using FinanceManagerAPI.ViewModels;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>?> GetAll();
        public Task<CategoryViewModel?> GetById(int id);
        public Task<bool> Create(CategoryCreateDto model);
        public Task<bool> Update(CategoryUpdateDto expectedEntityValues);
        public Task<bool> Delete(int? id);
    }
}
