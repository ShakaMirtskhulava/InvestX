namespace GHotel.Application.Models;

#pragma warning disable CS8618

public class ProjectResponseModel
{
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal RequiredBudget { get; set; }
    public decimal CurrentBudget { get; set; }

    public int BusinessId { get; set; }
    public List<ImageResponseModel> Images { get; set; }
}
