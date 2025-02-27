using Advert.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure.Repositories;

public class AdvertRepository(AdvertDbContext context) : IAdvertRepository
{
    public async Task<Domain.Entities.Advert> CreateAsync(Domain.Entities.Advert advert, CancellationToken cancellationToken = default)
    {
        var result = await context.Adverts.AddAsync(advert, cancellationToken);
        return result.Entity;
    }

    public async Task<Domain.Entities.Advert?> GetByIdAsync(int advertId, CancellationToken cancellationToken = default)
    {
        var result = await context.Adverts
            .Include(advert => advert.PlaceCity)
            .Include(advert => advert.PlaceRegion)
            .Include(advert => advert.PlaceCountry)
            .Include(advert => advert.AdvertPhoneNumbers)
            .Include(advert => advert.AdvertPhotos)
            .Include(advert => advert.AdvertPublicStatus)
            .Include(advert => advert.AdvertPrivateStatus)
            .FirstOrDefaultAsync(advert => advert.Id == advertId, cancellationToken);
        return result;
    }
}