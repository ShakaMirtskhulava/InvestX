namespace GHotel.API.Models;

#pragma warning disable CS8618

public class UpdateProjectModel
{
    public string ProjectName { get; set; }
    public IFormFileCollection Images { get; set; }
}
