using System.IdentityModel.Tokens.Jwt;
using ImageGallery.API.Authorization;
using ImageGallery.Infrastructure.Authorization;
using ImageGallery.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ImageGallery.API;

public static class DependencyInjection
{

    public static IServiceCollection AddApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpContextAccessor();

        services.AddOptions<DatabaseCredentialsOptions>()
            .Bind(configuration.GetSection(DatabaseCredentialsOptions.SectionName));
        
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddJwtBearer(options =>
            // {
            //     options.Authority = "https://localhost:5001";
            //     options.Audience = "image-gallery-api";
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         NameClaimType = "given_name",
            //         RoleClaimType = "role",
            //         ValidTypes = new[] { "at+jwt" }
            //     };
            // });
            .AddOAuth2Introspection(options =>
            {
                options.Authority = "https://localhost:5001";
                options.ClientId = "image-gallery-api";
                options.ClientSecret = "apisecret";
                options.NameClaimType = "given_name";
                options.RoleClaimType = "role";
            });
        
        services.AddScoped<IAuthorizationHandler, MustOwnImageHandler>();
        services.AddAuthorization(options =>
        {   
            options.AddPolicy("UserCanAddImage", AuthorizationPolicies.CanAddImage());
            options.AddPolicy("ClientApplicationCanWrite", builder =>
            {
                builder.RequireClaim("scope", "image-gallery-api.write");
            });
            options.AddPolicy("MustOwnImage", builder =>
            {
                builder.RequireAuthenticatedUser();
                builder.AddRequirements(new MustOwnImageRequirement());

            });
            

        });

        return services;

    }
}