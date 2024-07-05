using System.Security.Claims;
using Asp.Versioning;
using GHotel.API.Models;
using GHotel.Application.Models;
using GHotel.Application.Services.Project;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Controllers.V1;

[ApiController]
[Route("v{version:apiversion}/[controller]")]
[ApiVersion(1)]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;
    private readonly IProjectService _projectService;

    public ProjectController(ILogger<ProjectController> logger, IProjectService projectService)
    {
        _logger = logger;
        _projectService = projectService;
    }

    [HttpGet("{name}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ProjectResponseModel> GetByName(string name, CancellationToken cancellationToken)
    {
        var project = await _projectService.GetByName(name, cancellationToken).ConfigureAwait(false);
        return project;
    }

    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ProjectResponseModel> Create(ProjectRequestModel request, CancellationToken cancellationToken)
    {
        var project = await _projectService.Create(request, cancellationToken).ConfigureAwait(false);
        return project;
    }

    [Authorize]
    [HttpPut]
    public async Task<ProjectResponseModel> Update([FromForm] UpdateProjectModel updateRoomModel, CancellationToken cancellationToken)
    {
        List<ImageRequestModel> images = new();
        foreach (var image in updateRoomModel.Images)
        {
            var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            images.Add(new ImageRequestModel { Data = memoryStream.ToArray(), FileExtension = image.FileName.Split('.').Last() });
        }
        var currentUserEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        return await _projectService.UploadImages(currentUserEmail, updateRoomModel.ProjectName,images, cancellationToken).ConfigureAwait(false);
    }

    [Authorize]
    [HttpPost("Invest")]
    public async Task Invest(InvestCreateModel model, CancellationToken cancellationToken)
    {
        var request = model.Adapt<InvestRequestModel>();
        var eamil = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value!;
        request.InvestorEmail = eamil;

        await _projectService.Invest(request, cancellationToken).ConfigureAwait(false);
    }

}
