using ImageGallery.Application.Abstractions;
using ImageGallery.Domain.Images;
using ImageGallery.Infrastructure.Authentication;
using ImageGallery.Infrastructure.Data;
using ImageGallery.Infrastructure.Images;
using ImageGallery.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ImageGallery.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        string environmentWebRootPath)
    {
        services.AddScoped<IGalleryRepository, GalleryRepository>();
        services.AddScoped<IImageFileService>(x => new ImageFileService(environmentWebRootPath));
        services.AddOptions<ImageServiceOptions>().Bind(configuration.GetSection("ImageService"));
        services.AddOptions<IdpServiceOptions>().Bind(configuration.GetSection(IdpServiceOptions.SectionName));


        services.AddDbContext<ApplicationDbContext>((provider, config) =>
        {
            var databaseCredentials = provider.GetService<IOptions<DatabaseCredentialsOptions>>()!.Value;
            var connectionStringBuilder = new SqlConnectionStringBuilder(databaseCredentials.ConnectionString)
            {
                UserID = databaseCredentials.DbUser,
                Password = databaseCredentials.DbPassword,
                InitialCatalog = databaseCredentials.InitialCatalog,
                DataSource = databaseCredentials.DataSource,
                TrustServerCertificate = true
            };
            config.UseSqlServer(connectionStringBuilder.ConnectionString);
        });

        services.AddAccessTokenManagement();
        services.AddHttpClient<IImageHttpService, ImageHttpService>((serviceProvider, httpClient) =>
        {
            var imageServiceOptions = serviceProvider.GetRequiredService<IOptions<ImageServiceOptions>>().Value;

            httpClient.BaseAddress = new Uri(imageServiceOptions.BaseAddress);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }).AddUserAccessTokenHandler();

        services.AddHttpClient<IIdpService, IdpService>((serviceProvider, httpClient) =>
        {
            var idpServiceOptions = serviceProvider.GetRequiredService<IOptions<IdpServiceOptions>>().Value;

            httpClient.BaseAddress = new Uri(idpServiceOptions.BaseAddress);

        });


        return services;
    }
}