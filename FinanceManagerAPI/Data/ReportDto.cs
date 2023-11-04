using FinanceManagerAPI.Data.Operation;

namespace FinanceManagerAPI.Data
{
    public class ReportDto
    {
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpense { get; set; }
        public List<OperationUpdateDto>? operationsForPeriod { get; set; }
    }
}
