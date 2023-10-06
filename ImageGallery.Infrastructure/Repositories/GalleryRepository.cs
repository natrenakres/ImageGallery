using ImageGallery.Domain.Images;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Infrastructure.Repositories;

public class GalleryRepository : IGalleryRepository
{
    private readonly ApplicationDbContext _context;

    public GalleryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> ImageExistsAsync(Guid id)
    {
        return await _context.Images.AnyAsync(i => i.Id == id);
    }       

    public async Task<Image?> GetImageAsync(Guid id)
    {
        return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
    }
  
    public async Task<IEnumerable<Image>> GetImagesAsync()
    {
        return await _context.Images
            .OrderBy(i => i.Title).ToListAsync();
    }

    public async Task<bool> IsImageOwnerAsync(Guid id, string ownerId)
    {
        return await _context.Images
            .AnyAsync(i => i.Id == id && i.OwnerId == ownerId);
    }
        
    public void AddImage(Image image)
    {
        _context.Images.Add(image);
    }

    public void UpdateImage(Image image)
    {
        // no code in this implementation
    }

    public void DeleteImage(Image image)
    {
        _context.Images.Remove(image);

        // Note: in a real-life scenario, the image itself potentially should 
        // be removed from disk.  We don't do this in this demo
        // scenario to allow for easier testing / re-running the code
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    } 
}