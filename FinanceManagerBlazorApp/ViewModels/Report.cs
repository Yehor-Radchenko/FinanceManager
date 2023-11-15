namespace FinanceManagerBlazorApp.ViewModels
{
    public class Report
    {
        public decimal? TotalIncome { get; set; }
        public decimal? TotalExpense { get; set; }
        public List<Operation>? operationsForPeriod { get; set; }
    }
}
