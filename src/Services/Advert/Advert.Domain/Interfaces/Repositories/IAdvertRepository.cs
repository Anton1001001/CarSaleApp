namespace Advert.Domain.Interfaces.Repositories;

public interface IAdvertRepository
{
    Task<Entities.Advert> CreateAsync(Entities.Advert advert, CancellationToken cancellationToken);
    Task<Entities.Advert?> GetByIdAsync(int advertId, CancellationToken cancellationToken);
}