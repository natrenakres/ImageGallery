using ImageGallery.Domain.Images;

namespace ImageGallery.Infrastructure.Images;

public class ImageFileService : IImageFileService
{
    private readonly string _webRootPath;
    public ImageFileService(string environmentWebRootPath)
    {
        _webRootPath = environmentWebRootPath;
    }
    
    public async Task<string> CreateImage(byte[]  bytes)
    {
        var fileName = Guid.NewGuid() + ".jpg";
        var filePath = Path.Combine($"{_webRootPath}/images/{fileName}");

        await File.WriteAllBytesAsync(filePath, bytes);

        return fileName;
    }
}