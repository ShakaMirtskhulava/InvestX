namespace GHotel.Application.Models;

#pragma warning disable CS8618

public class InvestRequestModel
{
    public string InvestorEmail { get; set; }
    public string ProjectName { get; set; }
    public decimal Amount { get; set; }
}
