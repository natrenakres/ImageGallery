using FluentResults;
using ImageGallery.Application.Images.GetImages;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.AddImage;

public class AddImageCommandHandler : IRequestHandler<AddImageCommand, Result<ImageResponse>>
{
    private readonly IImageService _imageService;
    private readonly IGalleryRepository _galleryRepository;

    public AddImageCommandHandler(IImageService imageService, IGalleryRepository galleryRepository)
    {
        _imageService = imageService;
        _galleryRepository = galleryRepository;
    }

    public async Task<Result<ImageResponse>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var entity = Image.Create(request.Tile);
        
        var fileName = await _imageService.CreateImage(request.Bytes);
        entity.AddFileName(fileName);
        _galleryRepository.AddImage(entity);
        await _galleryRepository.SaveChangesAsync();

        return Result.Ok(new ImageResponse(entity.Id, entity.Title, entity.FileName));
    }
}