using FinanceManagerAPI.Data;
using FinanceManagerAPI.ViewModels;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IReportService
    {
        public Task<ReportViewModel> GetOperationsForPeriod(string date);
        public Task<ReportViewModel> GetOperationsForPeriod(string startDate, string endDate);
    }
}
