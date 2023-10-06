using FluentResults;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.RemoveImage;

public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommand, Result>
{
    private readonly IGalleryRepository _galleryRepository;

    public RemoveImageCommandHandler(IGalleryRepository galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }

    public async Task<Result> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        var imageFromRepo = await _galleryRepository.GetImageAsync(request.Id);

        if (imageFromRepo == null)
        {
            return Result.Fail(ImageErrors.NotFound);
        }

        _galleryRepository.DeleteImage(imageFromRepo);

        await _galleryRepository.SaveChangesAsync();

        return Result.Ok();
    }
}