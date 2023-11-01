using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<OperationCategory>?> GetAll();
        public Task<OperationCategory?> GetById(int id);
        public Task Create(OperationCategory model);
        public Task Update(OperationCategory expectedEntityValues);
        public Task Delete(int? id);
    }
}
