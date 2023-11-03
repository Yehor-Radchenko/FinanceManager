using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using System.Text.RegularExpressions;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IFinancialOperationService
    {
        public Task Create(OperationDto model);
        public Task Delete(int? id);
        public Task<IEnumerable<OperationDto>> GetAll();
        public Task<OperationDto?> GetById(int? id);
        public Task Update(OperationDto expectedEntityValues);
        public Task<ReportDto> GetOperationsForPeriod(string date);
        public Task<ReportDto> GetOperationsForPeriod(string startDate, string endDate);
    }
}
