using FinanceManagerAPI.Models;

namespace FinanceManagerAPI.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationType Type { get; set; }
    }
}
