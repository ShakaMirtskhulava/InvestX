using GHotel.Application.Utilities;

namespace GHotel.Application.Services.Image;

public class ImageService : IImageService
{
    private readonly IImageUtility _imageUtility;

    public ImageService(IImageUtility imageUtility)
    {
        _imageUtility = imageUtility;
    }

    public async Task<Stream> GetFile(string url, CancellationToken cancellationToken)
    {
        return await _imageUtility.ReadImageFromFile(url, cancellationToken).ConfigureAwait(false);
    }
}
