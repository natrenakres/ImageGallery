using FluentResults;
using ImageGallery.Domain.Images;
using MediatR;

namespace ImageGallery.Application.Images.GetImages;

public record GetImagesQuery : IRequest<Result<List<ImageResponse>>>;