using System.Security.Claims;
using Asp.Versioning;
using GHotel.Application.Models;
using GHotel.Application.Services.Share;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Controllers.V1;

[ApiController]
[Route("v{version:apiversion}/[controller]")]
[ApiVersion(1)]
public class ShareController : ControllerBase
{
    private readonly IShareService _shareService;

    public ShareController(IShareService shareService) => _shareService = shareService;

    [Authorize]
    [HttpGet("{projectName}")]
    public async Task<ProjectSharesResponseModel> Get(string projectName,CancellationToken cancellationToken)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        var response = await _shareService.GetSharesForUserAndProject(email,projectName, cancellationToken).ConfigureAwait(false);
        return response;
    }
}
