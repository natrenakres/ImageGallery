using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace ImageGallery.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", new []{"role"}),
            new IdentityResource("country", "The country you're living in", new List<string>(){ "country"})
        };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new ApiResource("image-gallery-api", "Image Gallery API", new []{"role", "country"})
        {
            Scopes =
            {
                "image-gallery-api.full-access",
                "image-gallery-api.read",
                "image-gallery-api.write"
            },
            ApiSecrets =
            {
                new Secret("apisecret".Sha256())
            }
        }
    };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("image-gallery-api.full-access"),
            new ApiScope("image-gallery-api.read"),
            new ApiScope("image-gallery-api.write"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery",
                ClientId = "image-gallery-client",
                AllowedGrantTypes = GrantTypes.Code,
                AccessTokenType = AccessTokenType.Reference,
                AllowOfflineAccess = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                AccessTokenLifetime = 120,
                //AuthorizationCodeLifetime = ...
                //IdentityTokenLifetime = ...
                RedirectUris = new List<string>()
                {
                    "https://localhost:4001/signin-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    //"image-gallery-api.full-access",
                    "image-gallery-api.read",
                    "image-gallery-api.write",
                    "country"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256()) 
                }
            }
        };
}