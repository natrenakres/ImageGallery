using ImageGallery.Domain.Images;
using ImageGallery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Image> Images { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}