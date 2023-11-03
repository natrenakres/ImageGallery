using ImageGallery.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGallery.Client.Controllers;

public class AuthenticationController : Controller
{
    private readonly IIdpService _idpService;

    public AuthenticationController(IIdpService idpService)
    {
        _idpService = idpService;
    }


    [Authorize]
    public async Task Logout()
    {
        await _idpService.RevokeTokensAsync();

        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignOutAsync(
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    public IActionResult AccessDenied()
    {

        return View(); 
    }
}