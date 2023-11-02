using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.Data
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationType Type { get; set; }
    }
}
