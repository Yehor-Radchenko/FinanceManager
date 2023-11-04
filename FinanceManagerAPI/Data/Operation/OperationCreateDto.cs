namespace FinanceManagerAPI.Data.Operation
{
    public class OperationCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MoneyAmount { get; set; }
        public DateTime DateTime { get; set; }
        public int CategoryId { get; set; }
    }
}
