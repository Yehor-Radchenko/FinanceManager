using FinanceManagerCommon.Data;
using FinanceManagerAPI.Models;
using FinanceManagerCommon.ViewModels;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IFinancialOperationService
    {
        public Task<bool> Create(OperationCreateDto model);
        public Task<bool> Delete(int? id);
        public Task<IEnumerable<OperationViewModel>> GetAll();
        public Task<OperationViewModel?> GetById(int? id);
        public Task<bool> Update(OperationUpdateDto expectedEntityValues);
    }
}
