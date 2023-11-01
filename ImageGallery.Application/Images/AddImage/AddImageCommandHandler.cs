using FluentResults;
using ImageGallery.Application.Images.GetImages;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.AddImage;

public class AddImageCommandHandler : IRequestHandler<AddImageCommand, Result<ImageResponse>>
{
    private readonly IImageFileService _imageFileService;
    private readonly IGalleryRepository _galleryRepository;

    public AddImageCommandHandler(IImageFileService imageFileService, IGalleryRepository galleryRepository)
    {
        _imageFileService = imageFileService;
        _galleryRepository = galleryRepository;
    }

    public async Task<Result<ImageResponse>> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var entity = Image.Create(request.Tile);
        
        var fileName = await _imageFileService.CreateImage(request.Bytes);
        entity.AddFileName(fileName);
        _galleryRepository.AddImage(entity);
        await _galleryRepository.SaveChangesAsync();

        return Result.Ok(new ImageResponse(entity.Id, entity.Title, entity.FileName));
    }
}