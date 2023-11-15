using FinanceManagerCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagerCommon.Data
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
