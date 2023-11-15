namespace FinanceManagerCommon.ViewModels
{
    public class ReportViewModel
    {
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpense { get; set; }
        public List<OperationViewModel>? operationsForPeriod { get; set; }
    }
}
