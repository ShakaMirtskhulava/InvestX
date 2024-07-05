using System.ComponentModel.DataAnnotations;
using GHotel.Domain.Enums;

namespace GHotel.Domain.Entities;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;
    [Required]
    public Currency Currency { get; set; }

    public string UserPersonalNumber { get; set; }
    public User? User { get; set; }

    public string ProjectName { get; set; }
    public Project? Project { get; set; }
}
