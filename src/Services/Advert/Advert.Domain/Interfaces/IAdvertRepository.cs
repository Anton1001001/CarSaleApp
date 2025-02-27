namespace Advert.Domain.Interfaces;

public interface IAdvertRepository
{
    Task<Entities.Advert> CreateAsync(Entities.Advert advert, CancellationToken cancellationToken);
    Task<Entities.Advert?> GetByIdAsync(int advertId, CancellationToken cancellationToken);
}