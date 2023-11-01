using ImageGallery.Application.Abstractions;
using ImageGallery.Application.Images.AddImage;
using ImageGallery.Application.Images.EditImage;
using ImageGallery.Application.Images.GetImages;
using Newtonsoft.Json;

namespace ImageGallery.Infrastructure.Images;

internal sealed class ImageHttpService : IImageHttpService
{
    private readonly HttpClient _httpClient;

    public ImageHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ImageResponse>> GetImages()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/images/");

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        
        response.EnsureSuccessStatusCode();

        
        var responseStream = await response.Content.ReadAsStringAsync();
        if (responseStream is null)
        {
            return new List<ImageResponse>();    
        }
            
        var images = JsonConvert.DeserializeObject<List<ImageResponse>>(responseStream);
        return images ?? new List<ImageResponse>();

    }

    public async Task<ImageResponse> GetImage(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/images/{id}");

        var response = await _httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        var deserializedImage = await System.Text.Json.JsonSerializer.DeserializeAsync<ImageResponse>(responseStream);

        if (deserializedImage == null)
        {
            throw new Exception("Deserialized image must not be null.");
        }

        return deserializedImage;
    }


    public async Task EditImage(Guid id, EditImageRequest editImageRequest)
    {
        // serialize it
        var serializedImageForUpdate = System.Text.Json.JsonSerializer.Serialize(editImageRequest);


        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/images/{id}")
        {
            Content = new StringContent(serializedImageForUpdate, System.Text.Encoding.Unicode,
                "application/json")
        };

        var response = await _httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveImage(Guid id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/images/{id}");

        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddImage(AddImageRequest? image)
    {
        // serialize it
        var serializedImage = System.Text.Json.JsonSerializer.Serialize(image);

        var request = new HttpRequestMessage(HttpMethod.Post, $"/api/images")
        {
            Content = new StringContent(
                serializedImage,
                System.Text.Encoding.Unicode,
                "application/json")
        };

        var response = await _httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }
}