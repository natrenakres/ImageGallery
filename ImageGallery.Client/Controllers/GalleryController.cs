
using ImageGallery.Application.Images.GetImages;
using ImageGallery.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ImageGallery.Client.Controllers;

public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GalleryController> _logger;

    public GalleryController(IHttpClientFactory httpClientFactory, ILogger<GalleryController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var httpClient = _httpClientFactory.CreateClient("APIClient");

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/images/");

        var response = await httpClient.SendAsync(
            request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        
            var responseStream = await response.Content.ReadAsStringAsync();
            if (responseStream is null)
            {
                return View(new GalleryIndexViewModel(new List<ImageResponse>()));    
            }
            
            var images = JsonConvert.DeserializeObject<List<ImageResponse>>(responseStream);
            return View(new GalleryIndexViewModel(images ?? new List<ImageResponse>()));
        
    }
    
    public async Task<IActionResult> EditImage(Guid id)
        {

            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"/api/images/{id}");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var deserializedImage = await System.Text.Json.JsonSerializer.DeserializeAsync<ImageResponse>(responseStream);

                if (deserializedImage == null)
                {
                    throw new Exception("Deserialized image must not be null.");
                }

                var editImageViewModel = new EditImageViewModel()
                {
                    Id = deserializedImage.Id,
                    Title = deserializedImage.Title
                };

                return View(editImageViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(EditImageViewModel editImageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create an ImageForUpdate instance
            var imageForUpdate = new ImageRequest(editImageViewModel.Title);

            // serialize it
            var serializedImageForUpdate = System.Text.Json.JsonSerializer.Serialize(imageForUpdate);

            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"/api/images/{editImageViewModel.Id}")
            {
                Content = new StringContent(
                    serializedImageForUpdate,
                    System.Text.Encoding.Unicode,
                    "application/json")
            };

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Delete,
                $"/api/images/{id}");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddImageViewModel addImageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // create an ImageForCreation instance
            AddImageRequest? imageForCreation = null;

            // take the first (only) file in the Files list
            var imageFile = addImageViewModel.Files.First();

            if (imageFile.Length > 0)
            {
                using (var fileStream = imageFile.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    imageForCreation = new AddImageRequest(
                        addImageViewModel.Title, ms.ToArray());
                }
            }

            // serialize it
            var serializedImageForCreation = System.Text.Json.JsonSerializer.Serialize(imageForCreation);

            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"/api/images")
            {
                Content = new StringContent(
                    serializedImageForCreation,
                    System.Text.Encoding.Unicode,
                    "application/json")
            };

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
}