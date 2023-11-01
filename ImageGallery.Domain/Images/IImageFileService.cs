namespace ImageGallery.Domain.Images;

public interface IImageFileService
{
    Task<string> CreateImage(byte[] bytes);
}