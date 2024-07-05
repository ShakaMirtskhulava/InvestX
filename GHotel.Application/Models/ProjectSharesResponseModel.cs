namespace GHotel.Application.Models;

public class ProjectSharesResponseModel
{
    public double TotalSharePercentage { get; set; }
    public List<ShareResponseModel> Shares { get; set; }
}
