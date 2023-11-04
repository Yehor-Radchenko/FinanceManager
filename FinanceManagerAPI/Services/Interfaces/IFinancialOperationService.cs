using FinanceManagerAPI.Data;
using FinanceManagerAPI.Data.Operation;
using FinanceManagerAPI.Models;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IFinancialOperationService
    {
        public Task<bool> Create(OperationCreateDto model);
        public Task<bool> Delete(int? id);
        public Task<IEnumerable<OperationUpdateDto>> GetAll();
        public Task<OperationUpdateDto?> GetById(int? id);
        public Task<bool> Update(OperationUpdateDto expectedEntityValues);
    }
}
