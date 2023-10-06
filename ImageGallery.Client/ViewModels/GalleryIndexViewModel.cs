using ImageGallery.Application.Images.GetImages;

namespace ImageGallery.Client.ViewModels;

public class GalleryIndexViewModel
{
    public IEnumerable<ImageResponse> Images { get; private set; }

    public GalleryIndexViewModel(IEnumerable<ImageResponse> images)
    {
        Images = images;
    }
}