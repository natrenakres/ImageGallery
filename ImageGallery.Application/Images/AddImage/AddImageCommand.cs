using FluentResults;
using ImageGallery.Application.Images.GetImages;
using MediatR;

namespace ImageGallery.Application.Images.AddImage;

public record AddImageCommand(string Tile, byte[] Bytes) : IRequest<Result<ImageResponse>>;
