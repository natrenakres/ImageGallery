namespace ImageGallery.API.Controllers;

public record AddImageRequest(string Title, byte[] Bytes);