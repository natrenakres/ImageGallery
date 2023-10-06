using ImageGallery.Client;
using ImageGallery.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddClient(builder.Configuration)
        .AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Gallery}/{action=Index}/{id?}");

    app.Run();
}
