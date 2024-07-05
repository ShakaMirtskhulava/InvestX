using System.ComponentModel.DataAnnotations;
namespace GHotel.Domain.Entities;

#pragma warning disable CS8618

public class MyImage
{
    [Key]
    public string Url { get; set; }
}
