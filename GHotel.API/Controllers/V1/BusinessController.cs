using System.Security.Claims;
using Asp.Versioning;
using GHotel.API.Models;
using GHotel.Application.Models;
using GHotel.Application.Services.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Controllers.V1;

[ApiController]
[Route("v{version:apiversion}/[controller]")]
[ApiVersion(1)]
public class BusinessController : ControllerBase
{
    private readonly ILogger<BusinessController> _logger;
    private readonly IBusinessService _businessService;

    public BusinessController(ILogger<BusinessController> logger, IBusinessService businessService)
    {
        _logger = logger;
        _businessService = businessService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<BusinessResponseModel>> GetAll(CancellationToken cancellationToken)
    {
        var businesses = await _businessService.GetAll(cancellationToken).ConfigureAwait(false);
        return businesses;
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusinessResponseModel>> GetByIdWithProjects(int id, CancellationToken cancellationToken)
    {
        var business = await _businessService.GetByIdWithProjects(id, cancellationToken).ConfigureAwait(false);
        return business;
    }

    [Authorize]
    [HttpPut]
    [Produces("application/json")]
    public async Task<BusinessResponseModel> Update([FromForm] UpdateBusinessModel updateBusinessModel, CancellationToken cancellationToken)
    {
        var memoryStream = new MemoryStream();
        await updateBusinessModel.Image.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
        var image = new ImageRequestModel { Data = memoryStream.ToArray(), FileExtension = updateBusinessModel.Image.FileName.Split('.').Last() };

        var currentUserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        return await _businessService.UploadImages(currentUserEmail, updateBusinessModel.BusinessId, image, cancellationToken).ConfigureAwait(false);
    }

}
