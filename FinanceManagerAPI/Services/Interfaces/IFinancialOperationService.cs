using FinanceManagerAPI.Models;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IFinancialOperationService
    {
        public Task Create(FinancialOperation model);
        public Task Delete(int? id);
        public Task<IEnumerable<FinancialOperation>> GetAll();
        public Task<FinancialOperation?> GetById(int? id);
        public Task Update(FinancialOperation expectedEntityValues);
    }
}
