using GHotel.Application.Exceptions;
using GHotel.Application.Models;
using GHotel.Application.Utilities;

namespace GHotel.Infrastructure.Utilities;

public class ImageUtility : IImageUtility
{
    private readonly string _imagesDirectoryPath;

    public ImageUtility()
    {
        _imagesDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
    }

    public async Task<string> SaveImageToFile(ImageRequestModel imageRequestModel, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(_imagesDirectoryPath))
            Directory.CreateDirectory(_imagesDirectoryPath);

        var uniqueImageFileName = $"{Guid.NewGuid()}.{imageRequestModel.FileExtension}";
        var imagePath = Path.Combine(_imagesDirectoryPath, uniqueImageFileName);
        using var fileStream = new FileStream(imagePath, FileMode.Create);

        using var memoryStream = new MemoryStream(imageRequestModel.Data);
        await memoryStream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);

        return uniqueImageFileName;
    }

    public async Task<Stream> ReadImageFromFile(string imageFileUrl, CancellationToken cancellationToken)
    {
        var imagePath = Path.Combine(_imagesDirectoryPath, imageFileUrl);
        if (!File.Exists(imagePath))
            throw new NotFoundException($"Couldn't found the Image with Filename: {imageFileUrl}");

        var memoryStream = new MemoryStream();
        using var fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);

        await fileStream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);

        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    public void DeleteImageFile(string imageFileUrl)
    {
        var imagePath = Path.Combine(_imagesDirectoryPath, imageFileUrl);
        if (File.Exists(imagePath))
            File.Delete(imagePath);
    }

    public void DeleteImageFileRange(List<string> imageFileUrls)
    {
        foreach (var imageFileUrl in imageFileUrls)
            DeleteImageFile(imageFileUrl);
    }
}
