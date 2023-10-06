using FluentResults;
using ImageGallery.Application.Images.GetImages;
using MediatR;

namespace ImageGallery.Application.Images.GetImage;

public record GetImageQuery(Guid Id) : IRequest<Result<ImageResponse>>;