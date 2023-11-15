using Azure;
using FinanceManagerCommon.Data;
using FinanceManagerAPI.Models;
using FinanceManagerCommon.ViewModels;

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
