using GHotel.Application.Models;

namespace GHotel.Application.Services.Project;

public interface IProjectService
{
    Task<ProjectResponseModel> Create(ProjectRequestModel request, CancellationToken cancellationToken);
    Task<ProjectResponseModel> GetByName(string name, CancellationToken cancellationToken);
    Task<ProjectResponseModel> UploadImages(string userPersonalNumber, string projectName, List<ImageRequestModel> images, CancellationToken cancellationToken);
    Task Invest(InvestRequestModel request, CancellationToken cancellationToken);
}
