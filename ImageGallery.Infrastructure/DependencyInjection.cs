using ImageGallery.Domain.Images;
using ImageGallery.Infrastructure.Repositories;
using ImageGallery.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGallery.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        string environmentWebRootPath)
    {
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        
        services.AddScoped<IImageService>(x => new ImageService(environmentWebRootPath));

        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("Database"));
        });


        return services;
    }
}