using FluentResults;
using MediatR;

namespace ImageGallery.Application.Images.EditImage;

public record EditImageCommand(Guid Id, string Title) : IRequest<Result>;
