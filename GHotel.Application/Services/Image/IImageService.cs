namespace GHotel.Application.Services.Image;

public interface IImageService
{
    Task<Stream> GetFile(string url, CancellationToken cancellationToken);
}
