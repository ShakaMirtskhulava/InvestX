using GHotel.Application.Models;

namespace GHotel.Application.Services.Share;

public interface IShareService
{
    Task<ProjectSharesResponseModel> GetSharesForUserAndProject(string email, string projectName, CancellationToken cancellationToken);
}
