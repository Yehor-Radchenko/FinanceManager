namespace FinanceManagerAPI.ViewModels
{
    public class OperationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MoneyAmount { get; set; }
        public DateTime DateTime { get; set; }
        public int CategoryId { get; set; }
    }
}
