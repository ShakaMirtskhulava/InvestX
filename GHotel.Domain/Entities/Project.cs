using System.ComponentModel.DataAnnotations;

namespace GHotel.Domain.Entities;

#pragma warning disable CS8618

public class Project
{
    [Key]
    public string Name { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal RequiredBudget { get; set; }
    [Required]
    public decimal CurrentBudget { get; set; }

    public int BusinessId { get; set; }
    public Business? Business { get; set; }
    public List<Share>? Shares { get; set; }
    public List<MyImage>? Images { get; set; }
    public List<Transaction>? Transactions { get; set; }
}
