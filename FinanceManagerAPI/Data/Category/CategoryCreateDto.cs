using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.Data.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public OperationType Type { get; set; }
    }
}
