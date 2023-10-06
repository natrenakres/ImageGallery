using ImageGallery.Domain.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageGallery.Infrastructure.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(150);
        builder.Property(x => x.FileName).HasMaxLength(200);
        builder.Property(x => x.OwnerId).HasMaxLength(50);
    }
}