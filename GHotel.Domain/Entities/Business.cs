using System.ComponentModel.DataAnnotations;

namespace GHotel.Domain.Entities;

#pragma warning disable CS8618

public class Business
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string CreationDate { get; set; }

    public string UserPersonalNumber { get; set; }
    public User? User { get; set; }

    public List<Project>? Projects { get; set; }

    public string? ImageUrl { get; set; }
    public MyImage? Image { get; set; }
}
