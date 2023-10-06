using FluentResults;
using ImageGallery.Application.Images.GetImages;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.GetImage;

public class GetImageQueryHandler : IRequestHandler<GetImageQuery, Result<ImageResponse>>
{
    private readonly IGalleryRepository _galleryRepository;

    public GetImageQueryHandler(IGalleryRepository galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }

    public async Task<Result<ImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var image = await _galleryRepository.GetImageAsync(request.Id);

        return image is null ? Result.Fail(ImageErrors.NotFound) : Result.Ok(new ImageResponse(image.Id, image.Title, image.FileName));
    }
}