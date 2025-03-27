using File.Core.Abstractions;
using File.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace File.DataAccess.Repositories;

public class PhotoRepository(FileDbContext context) : IPhotoRepository
{
    public async Task<List<int>> GetInactivePhotoIdsAsync(List<int> activePhotoIds,
        CancellationToken cancellationToken = default)
    {
        var allPhotoIds = await context.Photos
            .AsNoTracking()
            .Select(photo => photo.Id)
            .ToListAsync(cancellationToken);

        var inactivePhotoIds = allPhotoIds
            .Where(id => !activePhotoIds.Contains(id))
            .ToList();
        return inactivePhotoIds;
    }

    public async Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await context.Photos
            .AsNoTracking()
            .Include(photo => photo.Big)
            .Include(photo => photo.Medium)
            .Include(photo => photo.Small)
            .Include(photo => photo.ExtraSmall)
            .FirstOrDefaultAsync(photo => photo.Id == id, cancellationToken: cancellationToken);
        return response;
    }

    public async Task<IEnumerable<Photo>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        var response = await context.Photos
            .AsNoTracking()
            .Include(photo => photo.Big)
            .Include(photo => photo.Medium)
            .Include(photo => photo.Small)
            .Include(photo => photo.ExtraSmall)
            .ToListAsync(cancellationToken);

        response = response.Where(photo => ids.Contains(photo.Id))
            .ToList();

        return response;
    }


    public async Task<Photo> AddAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        await context.Photos.AddAsync(photo, cancellationToken);
        return await Task.FromResult(photo);
    }

    public async Task<Photo> UpdateAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        context.Photos.Update(photo);
        return await Task.FromResult(photo);
    }

    public async Task DeleteAsync(List<int> ids, CancellationToken cancellationToken = default)
    {
        var allPhotoIds = await context.Photos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var photos = allPhotoIds
            .Where(p => ids.Contains(p.Id))
            .ToList();

        if (photos.Count != 0)
        {
            context.Photos.RemoveRange(photos);
        }
    }
}