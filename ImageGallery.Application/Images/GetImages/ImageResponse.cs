namespace ImageGallery.Application.Images.GetImages;

public class ImageResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    
    public ImageResponse(Guid id, string title, string fileName)
    {
        Id = id;
        Title = title;
        FileName = fileName;
    }
    
    
}