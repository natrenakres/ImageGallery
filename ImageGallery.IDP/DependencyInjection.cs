namespace ImageGallery.IDP;

public static class DependencyInjection
{
    public static IServiceCollection AddIDP(this IServiceCollection services)
    {
        // uncomment if you want to add a UI
        services.AddRazorPages();

        services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);
        


        return services;
    }
}