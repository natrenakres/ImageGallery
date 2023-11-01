using System.Text;
using ImageGallery.Application.Abstractions;
using ImageGallery.Application.Images.AddImage;
using ImageGallery.Application.Images.EditImage;
using ImageGallery.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ImageGallery.Client.Controllers;

[Authorize]
public class GalleryController : Controller
{
    private readonly ILogger<GalleryController> _logger;
    private readonly IImageHttpService _imageHttpService;

    public GalleryController(ILogger<GalleryController> logger, IImageHttpService imageHttpService)
    {
        _logger = logger;
        _imageHttpService = imageHttpService;
    }

    public async Task<IActionResult> Index()
    {
        await LogIdentityInformation();
        var response = await _imageHttpService.GetImages();
        return View(new GalleryIndexViewModel(response));
    }

    public async Task<IActionResult> EditImage(Guid id)
    {
        var deserializedImage = await _imageHttpService.GetImage(id);

        var editImageViewModel = new EditImageViewModel
        {
            Id = deserializedImage.Id,
            Title = deserializedImage.Title
        };

        return View(editImageViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditImage(EditImageViewModel editImageViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var imageForUpdate = new EditImageRequest(editImageViewModel.Title);
        await _imageHttpService.EditImage(editImageViewModel.Id, imageForUpdate);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteImage(Guid id)
    {
        await _imageHttpService.RemoveImage(id);

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "PayingUser")]
    public IActionResult AddImage()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "PayingUser")]
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
            await using var fileStream = imageFile.OpenReadStream();
            using var ms = new MemoryStream();
            await fileStream.CopyToAsync(ms);
            imageForCreation = new AddImageRequest(
                addImageViewModel.Title, ms.ToArray());
        }

        await _imageHttpService.AddImage(imageForCreation);

        return RedirectToAction("Index");
    }

    public async Task LogIdentityInformation()
    {
        var identityToken = await HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        var userClaimsStringBuilder = new StringBuilder();
        foreach (var claim in User.Claims)
        {
            userClaimsStringBuilder.AppendLine($"Claim type {claim.Type} - Claim value: {claim.Value}");
        }

        _logger.LogInformation($"Identity token & user claims: " + $"\n{identityToken} \n {userClaimsStringBuilder}");
    }
}