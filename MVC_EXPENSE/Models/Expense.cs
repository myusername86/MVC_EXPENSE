using System.ComponentModel.DataAnnotations;

namespace MVC_EXPENSE.Models
{
    public class Expense
    {
        public int Id{ get; set; }

        [Required]
        public string ? Description { get; set; }

        public int value{ get; set; }
    }
}
