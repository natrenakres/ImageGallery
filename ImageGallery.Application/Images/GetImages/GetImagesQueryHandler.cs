using FluentResults;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.GetImages;

public class GetImagesQueryHandler : IRequestHandler<GetImagesQuery, Result<List<ImageResponse>>>
{
    private readonly IGalleryRepository _galleryRepository;

    public GetImagesQueryHandler(IGalleryRepository galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }

    
    public async Task<Result<List<ImageResponse>>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
    {
        var images = await _galleryRepository.GetImagesAsync();

        return Result.Ok(images.Select(i => new ImageResponse(i.Id, i.Title, i.FileName)).ToList());

    }
}