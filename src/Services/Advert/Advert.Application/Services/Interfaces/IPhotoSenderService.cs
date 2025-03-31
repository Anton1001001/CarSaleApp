namespace Advert.Application.Services.Interfaces;

public interface IPhotosSenderService
{
    Task SendAdvertsPhotosIdsAsync(CancellationToken cancellationToken);
}