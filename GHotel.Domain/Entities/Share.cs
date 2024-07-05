using System.ComponentModel.DataAnnotations;

namespace GHotel.Domain.Entities;

#pragma warning disable CS8618

public class Share
{
    [Key]
    public int Id { get; set; }
    public double SharePercentage { get; set; }

    public string UserPersonalNumber { get; set; }
    public User? User { get; set; }
    public string ProjectName { get; set; }
    public Project? Project { get; set; }
}
