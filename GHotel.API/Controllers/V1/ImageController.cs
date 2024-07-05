using Asp.Versioning;
using GHotel.Application.Services.Image;
using Microsoft.AspNetCore.Mvc;

namespace GHotel.API.Controllers.V1;

[ApiController]
[Route("v{version:apiversion}/[controller]")]
[ApiVersion(1)]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> GetImage(string url, CancellationToken cancellationToken)
    {
        var imageStream = await _imageService.GetFile(url, cancellationToken).ConfigureAwait(false);
        var imageExtension = Path.GetExtension(url);

        return File(imageStream, $"image/{imageExtension}");
    }
}
