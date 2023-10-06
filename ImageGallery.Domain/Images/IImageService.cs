namespace ImageGallery.Domain.Images;

public interface IImageService
{
    Task<string> CreateImage(byte[] bytes);
}