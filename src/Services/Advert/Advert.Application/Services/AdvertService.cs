using Newtonsoft.Json;
using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors;
using Advert.Application.Errors.Base;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Services;

public class AdvertService(
    IServiceScopeFactory serviceScopeFactory,
    IUnitOfWork unitOfWork,
    Processor<Domain.Entities.Advert, Result<AdvertResponse>> processor,
    Processor<Domain.Entities.Advert, Result<AdvertPreviewResponse>> processorPreview,
    IDistributedCache cache)
    : IAdvertService
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(6);

    public async Task<Result<AdvertResponse>> GetAdvertByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"Advert:Full:{id}";
        var cached = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (cached is not null)
        {
            var cachedResponse = JsonConvert.DeserializeObject<AdvertResponse>(cached);
            return cachedResponse!;
        }

        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(id, cancellationToken);
        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {id} was not found");
        }

        var response = await processor.HandleAsync(advert, cancellationToken);
        if (response is null || response.IsFailed)
        {
            return new InternalServerError(code: "Advert.GetById", message: "No processor for supplied vehicle type");
        }

        await cache.SetStringAsync(
            cacheKey,
            JsonConvert.SerializeObject(response.Value),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheDuration },
            cancellationToken);

        return response;
    }

    public async Task<Result<AdvertPreviewResponse>> GetAdvertPreviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"Advert:Preview:{id}";
        var cached = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (cached is not null)
        {
            var cachedResponse = JsonConvert.DeserializeObject<AdvertPreviewResponse>(cached);
            return cachedResponse!;
        }

        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(id, cancellationToken);
        if (advert is null)
        {
            return new AdvertNotFoundError(message: $"Advert with id: {id} was not found");
        }

        var response = await processorPreview.HandleAsync(advert, cancellationToken);
        if (response is null || response.IsFailed)
        {
            return new InternalServerError(code: "Advert.GetById", message: "No processor for supplied vehicle type");
        }

        await cache.SetStringAsync(
            cacheKey,
            JsonConvert.SerializeObject(response.Value),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = CacheDuration },
            cancellationToken);

        return response;
    }
}
