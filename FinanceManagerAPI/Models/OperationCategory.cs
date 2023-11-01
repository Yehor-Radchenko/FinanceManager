using System.ComponentModel.DataAnnotations;

namespace FinanceManagerAPI.Models
{
    public class OperationCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationType Type { get; set; }
        public List<FinancialOperation> FinancialOperations { get; set; }
    }
}
