namespace FinanceManagerBlazorApp.ViewModels
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MoneyAmount { get; set; }
        public DateTime DateTime { get; set; }
        public string CategoryName { get; set; }
    }
}
