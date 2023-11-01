namespace ImageGallery.Infrastructure.Images;

public sealed class ImageServiceOptions
{
    public string BaseAddress { get; init; } = string.Empty;
    public static string SectionName { get; set; } = "ImageService";
}