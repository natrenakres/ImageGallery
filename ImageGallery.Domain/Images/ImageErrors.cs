using FluentResults;

namespace ImageGallery.Domain.Images;

public static class ImageErrors 
{
    public static Error NotFound = new("The image cannot find");
}