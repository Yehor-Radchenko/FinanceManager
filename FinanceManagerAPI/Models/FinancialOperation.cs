using System.ComponentModel.DataAnnotations;

namespace FinanceManagerAPI.Models
{
    public class FinancialOperation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MoneyAmount
        {
            get { return MoneyAmount; }
            set
            {
                if (value > 0)
                    MoneyAmount = value;
                else
                    throw new ArgumentException("MoneyAmount cant be less than zero.");
            }
        }
        public DateTime DateTime { get; set; }
        public int CategoryId { get; set; }
        public OperationCategory Category { get; set; }
    }
}
