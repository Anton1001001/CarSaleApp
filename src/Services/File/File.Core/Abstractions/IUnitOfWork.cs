namespace File.Core.Abstractions;

public interface IUnitOfWork
{
    IPhotoRepository PhotoRepository { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}