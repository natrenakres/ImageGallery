using ImageGallery.Domain.Images;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Infrastructure.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>().HasData(
            Image.Create("An image by David")
                .AddFileName("3fbe2aea-2257-44f2-b3b1-3d8bacade89c.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("43de8b65-8b19-4b87-ae3c-df97e18bd461.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("46194927-ccda-432f-bc95-4820318c08c7.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("4cdd494c-e6e1-4af1-9e54-24a8e80ea2b4.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("5c20ca95-bb00-4ef1-8b85-c4b11e66eb54.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("6b33c074-65cf-4f2b-913a-1b2d4deb7050.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by David")
                .AddFileName("7e80a4c8-0a8a-4593-a16f-2e257294a1f9.jpg")
                .AddOwner("d860efca-22d9-47fd-8249-791ba61b07c7"),
            Image.Create("An image by Emma")
                .AddFileName("8d351bbb-f760-4b56-9d4e-e8d61619bf70.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma")
                .AddFileName("b2894002-0b7c-4f05-896a-856283012c87.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma")
                .AddFileName("cc412f08-2a7b-4225-b659-07fdb168302d.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma").AddFileName("cd139111-c82e-4bc8-9f7d-43a1059bfe73.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma")
                .AddFileName("dc3f39bf-d1da-465d-9ea4-935902c2e3d2.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma").AddFileName("e0e32179-109c-4a8a-bf91-3d65ff83ca29.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7"),
            Image.Create("An image by Emma")
                .AddFileName("fdfe7329-e05c-41fb-a7c7-4f3226d28c49.jpg")
                .AddOwner("b7539694-97e7-4dfe-84da-b4256e1ff5c7")
        );
    }
}