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
            new IdentityResource("roles", "Your role(s)", new []{"role"})
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientName = "Image Gallery",
                ClientId = "image-gallery-client",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = new List<string>()
                {
                    "https://localhost:4001/signin-oidc"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles"
                },
                ClientSecrets =
                {
                    new Secret("secret".Sha256()) 
                }
            }
        };
}