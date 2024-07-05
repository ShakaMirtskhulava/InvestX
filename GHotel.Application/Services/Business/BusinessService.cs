using GHotel.Application.Exceptions;
using GHotel.Application.Models;
using GHotel.Application.Repositories;
using GHotel.Application.Utilities;
using GHotel.Domain.Entities;
using Mapster;

namespace GHotel.Application.Services.Business;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IImageUtility _imageUtility;

    public BusinessService(IBusinessRepository businessRepository, IImageUtility imageUtility)
    {
        _businessRepository = businessRepository;
        _imageUtility = imageUtility;
    }

    public async Task<List<BusinessResponseModel>> GetAll(CancellationToken cancellationToken)
    {
        var business = await _businessRepository.GetAll(cancellationToken).ConfigureAwait(false);
        return business.Adapt<List<BusinessResponseModel>>();
    }

    public async Task<BusinessResponseModel> GetByIdWithProjects(int id, CancellationToken cancellationToken)
    {
        var targetBusiness = await _businessRepository.GetByIdWithProjects(id, cancellationToken).ConfigureAwait(false);
        if (targetBusiness == null)
            throw new NotFoundException("Business is not found");
        return targetBusiness.Adapt<BusinessResponseModel>();
    }

    public async Task<BusinessResponseModel> UploadImages(string currentUserEmail, int businessId, ImageRequestModel image, CancellationToken cancellationToken)
    {
        var targetBusiness = await _businessRepository.GetByIdWithUser(businessId, cancellationToken).ConfigureAwait(false);
        if (targetBusiness == null)
            throw new NotFoundException("Project not found");
        if (targetBusiness!.User!.Email != currentUserEmail)
            throw new Exception("You are not authorized to upload images for this business");;

        targetBusiness.Image = new MyImage { Url = await _imageUtility.SaveImageToFile(image, cancellationToken).ConfigureAwait(false) };

        await _businessRepository.Update(targetBusiness, cancellationToken).ConfigureAwait(false);
        return targetBusiness.Adapt<BusinessResponseModel>();
    }

}

