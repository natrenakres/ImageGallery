using FluentResults;
using MediatR;

namespace ImageGallery.Application.Images.RemoveImage;

public record RemoveImageCommand(Guid Id) : IRequest<Result>;