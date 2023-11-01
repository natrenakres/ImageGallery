using ImageGallery.Infrastructure.Data;

namespace ImageGallery.API;

public static class DependencyInjection
{

    public static IServiceCollection AddApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddOptions<DatabaseCredentialsOptions>()
            .Bind(configuration.GetSection(DatabaseCredentialsOptions.SectionName));

        return services;

    }
}