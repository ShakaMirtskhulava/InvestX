using System.ComponentModel.DataAnnotations;

namespace GHotel.Domain.Entities;

#pragma warning disable CS8618

public class User
{
    [Key]
    public string PersonalNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string PasswordHash { get; set; }

    public List<Business> Businesses { get; set; }
    public List<Share>? Shares { get; set; }
    public List<Transaction> Transactions { get; set; }
}
