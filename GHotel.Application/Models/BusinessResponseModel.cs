namespace GHotel.Application.Models;

#pragma warning disable CS8618

public class BusinessResponseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public ImageResponseModel? Image { get; set; }

    public List<ProjectResponseModel>? Projects { get; set; }
}
