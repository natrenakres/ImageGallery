using ImageGallery.Application.Images.AddImage;
using ImageGallery.Application.Images.EditImage;
using ImageGallery.Application.Images.GetImages;

namespace ImageGallery.Application.Abstractions;

public interface IImageHttpService
{
    Task<List<ImageResponse>> GetImages();
    Task<ImageResponse> GetImage(Guid id);
    Task EditImage(Guid id, EditImageRequest editImageRequest);
    Task AddImage(AddImageRequest? image);
    Task RemoveImage(Guid id);

}