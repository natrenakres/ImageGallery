using ImageGallery.Domain.Images;

namespace ImageGallery.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly string _webRootPath;
    public ImageService(string environmentWebRootPath)
    {
        _webRootPath = environmentWebRootPath;
    }
    
    public async Task<string> CreateImage(byte[]  bytes)
    {
        string fileName = Guid.NewGuid() + ".jpg";
        string filePath = Path.Combine($"{_webRootPath}/images/{fileName}");

        await System.IO.File.WriteAllBytesAsync(filePath, bytes);

        return fileName;
    }
}