namespace ImageGallery.Client.Controllers;

public record ImageRequest(string Title);

public record AddImageRequest(string Title, byte[] Bytes);