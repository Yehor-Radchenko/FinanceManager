using FinanceManagerAPI.Data;

namespace FinanceManagerAPI.Services.Interfaces
{
    public interface IReportService
    {
        public Task<ReportDto> GetOperationsForPeriod(string date);
        public Task<ReportDto> GetOperationsForPeriod(string startDate, string endDate);
    }
}
