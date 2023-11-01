using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace ImageGallery.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllersWithViews()
            .AddJsonOptions(configure =>
                configure.JsonSerializerOptions.PropertyNamingPolicy = null);
        
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
        services.AddAppAuthentication(configuration);

        return services;
    }

    private static IServiceCollection AddAppAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

                })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, (options) =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://localhost:5001/";
                options.ClientId = "image-gallery-client";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.Scope.Add("roles");
                // options.Scope.Add("openid"); By default added
                // options.Scope.Add("profile"); By default
                //options.CallbackPath = new PathString("signin-oidc"); By default
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.DeleteClaim("sid");
                options.ClaimActions.DeleteClaim("idp");
                options.ClaimActions.MapJsonKey("role", "role");
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    NameClaimType = "given_name",
                    RoleClaimType = "role"
                };
            });

        return services;
    }
}