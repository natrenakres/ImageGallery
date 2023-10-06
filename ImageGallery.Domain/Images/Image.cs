namespace ImageGallery.Domain.Images;

public class Image
{
    public Guid Id { get;  private set; }
    public string Title { get; private set; } = null!;
    public string FileName { get; private set; } = string.Empty;
    public string OwnerId { get; private set; } = string.Empty;

    private Image(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
    }

    public Image AddFileName(string fileName)
    {
        FileName = fileName;

        return this;
    }


    public static Image Create(string title)
    {
        var image = new Image(title);


        return image;
    }

    public Image AddOwner(string ownerId)
    {
        OwnerId = ownerId;
        
        return this;
    }

    public void Edit(string title)
    {
        Title = title;
    }
}