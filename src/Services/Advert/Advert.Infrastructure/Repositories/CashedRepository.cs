using System.Text.Json;
using Advert.Domain.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Advert.Infrastructure.Repositories;

public class CachedRepository<T>(IRepository<T> innerRepository, IDistributedCache cache) : IRepository<T> where T : class
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(6); 
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(T).Name}:id:{id}"; 
        var cachedData = await cache.GetStringAsync(cacheKey, cancellationToken); 

        if (cachedData is not null) 
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        var entity = await innerRepository.GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CacheDuration
            }, cancellationToken);
        }

        return entity;
    }


    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(T).Name}:all";
        var cachedData = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (cachedData is not null)
        {
            Console.WriteLine($"{cacheKey} cached");
            return JsonSerializer.Deserialize<List<T>>(cachedData) ?? [];
        }

        var entities = await innerRepository.GetAllAsync(cancellationToken);
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entities), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        }, cancellationToken);

        return entities;
    }
}