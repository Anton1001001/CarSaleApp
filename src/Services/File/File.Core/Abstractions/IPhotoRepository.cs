using File.Core.Models;

namespace File.Core.Abstractions;

public interface IPhotoRepository
{
    Task<List<int>> GetInactivePhotoIdsAsync(List<int> activePhotoIds, CancellationToken cancellationToken);
    Task<Photo?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<Photo>> GetByIdsAsync(List<int> ids, CancellationToken cancellationToken);
    Task<Photo> AddAsync(Photo photo, CancellationToken cancellationToken);
    Task<Photo> UpdateAsync(Photo photo, CancellationToken cancellationToken);
    Task DeleteAsync(List<int> id, CancellationToken cancellationToken);
}