using IdentityModel.Client;
using ImageGallery.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ImageGallery.Infrastructure.Authentication;

internal sealed class IdpService : IIdpService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    

    public IdpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private async Task<string> GetRevocationEndpointAsync()
    {
        var response = await _httpClient.GetDiscoveryDocumentAsync();

        if (response.IsError)
        {
            throw new Exception(response.Error);
        }

        return response.RevocationEndpoint;
    }


    public async Task RevokeTokensAsync()
    {
        var revocationEndpoint = await GetRevocationEndpointAsync();

        var accessTokenRevocationResponse = await _httpClient.RevokeTokenAsync(new()
        {
            Address = revocationEndpoint,
            ClientId = "image-gallery-clint",
            ClientSecret = "secret",
            Token = await _httpContextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)!
        });

        if (accessTokenRevocationResponse.IsError)
        {
            throw new Exception(accessTokenRevocationResponse.Error);
        }

        var refreshTokenRevocationResponse = await _httpClient.RevokeTokenAsync(new()
        {
            Address = revocationEndpoint,
            ClientId = "image-gallery-clint",
            ClientSecret = "secret",
            Token = await _httpContextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken)!
        });

        if (refreshTokenRevocationResponse.IsError)
        {
            throw new Exception(refreshTokenRevocationResponse.Error);
        }
    }
}

public sealed class IdpServiceOptions
{
    public string BaseAddress { get; init; } = string.Empty;
    
    public static string SectionName { get; set; } = "IdpService";
}