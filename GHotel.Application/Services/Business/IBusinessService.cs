using GHotel.Application.Models;

namespace GHotel.Application.Services.Business;

public interface IBusinessService
{
    Task<List<BusinessResponseModel>> GetAll(CancellationToken cancellationToken);
    Task<BusinessResponseModel> GetByIdWithProjects(int id, CancellationToken cancellationToken);
    Task<BusinessResponseModel> UploadImages(string currentUserEmail, int businessId, ImageRequestModel image, CancellationToken cancellationToken);
}
