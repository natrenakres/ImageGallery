using Microsoft.Net.Http.Headers;

namespace ImageGallery.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddControllersWithViews()
            .AddJsonOptions(configure => 
                configure.JsonSerializerOptions.PropertyNamingPolicy = null);
        
        services.AddHttpClient("APIClient", client =>
        {
            client.BaseAddress = new Uri(configuration["ImageGalleryAPIRoot"] ?? string.Empty);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });


        return services;
    }
}