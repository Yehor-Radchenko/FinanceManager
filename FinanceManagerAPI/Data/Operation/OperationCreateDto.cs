namespace FinanceManagerAPI.Data.Operation
{
    public class OperationCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        private decimal? _moneyAmount;
        public decimal? MoneyAmount
        {
            get { return _moneyAmount; }
            set
            {
                if (value.HasValue && value.Value >= 0)
                {
                    _moneyAmount = value;
                }
                else
                {
                    throw new ArgumentException("MoneyAmount cannot be less than zero.");
                }
            }
        }
        public DateTime DateTime { get; set; }
        public int CategoryId { get; set; }
    }
}
