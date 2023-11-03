namespace ImageGallery.Application.Abstractions;

public interface IIdpService
{
    Task RevokeTokensAsync();
}