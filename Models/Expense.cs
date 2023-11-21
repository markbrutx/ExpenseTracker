using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Models;

public class Expense
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount should be higher than 0")]
    public decimal Amount { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool IsDeleted { get; set; }
}