using FinanceManagerAPI.Data.Operation;

namespace FinanceManagerAPI.ViewModels
{
    public class ReportViewModel
    {
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpense { get; set; }
        public List<OperationUpdateDto>? operationsForPeriod { get; set; }
    }
}
