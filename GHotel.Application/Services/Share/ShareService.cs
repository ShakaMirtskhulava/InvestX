using GHotel.Application.Models;
using GHotel.Application.Repositories;
using Mapster;

namespace GHotel.Application.Services.Share;

public class ShareService : IShareService
{
    private readonly IShareRepository _shareRepository;

    public ShareService(IShareRepository shareRepository) => _shareRepository = shareRepository;

    public async Task<ProjectSharesResponseModel> GetSharesForUserAndProject(string email,string projectName,CancellationToken cancellationToken)
    {
        var shares = await _shareRepository.GetSharesForUserAndProject(email, projectName, cancellationToken).ConfigureAwait(false);
        ProjectSharesResponseModel projectSharesResponseModel = new()
        {
            TotalSharePercentage = shares.Sum(s => s.SharePercentage),
            Shares = shares.Adapt<List<ShareResponseModel>>()
        };

        return projectSharesResponseModel;
    }
}
