using FinanceManagerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagerAPI.Data.Category
{
    public class CategoryUpdateDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationType Type { get; set; }
    }
}
