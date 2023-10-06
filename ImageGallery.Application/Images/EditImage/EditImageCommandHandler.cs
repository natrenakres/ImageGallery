using FluentResults;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.EditImage;

public class EditImageCommandHandler : IRequestHandler<EditImageCommand, Result>
{
    private readonly IGalleryRepository _galleryRepository;

    public EditImageCommandHandler(IGalleryRepository galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }

    public async Task<Result> Handle(EditImageCommand request, CancellationToken cancellationToken)
    {
        var imageFromRepo = await _galleryRepository.GetImageAsync(request.Id);
        if (imageFromRepo == null)
        {
            return Result.Fail(ImageErrors.NotFound);
        }

        imageFromRepo.Edit(request.Title);

        _galleryRepository.UpdateImage(imageFromRepo);

        await _galleryRepository.SaveChangesAsync();

        return Result.Ok();
    }
}