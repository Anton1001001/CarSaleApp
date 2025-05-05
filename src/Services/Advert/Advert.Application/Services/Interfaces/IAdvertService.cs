using Advert.Application.Common.Advert.Models;
using FluentResults;

namespace Advert.Application.Services.Interfaces;

public interface IAdvertService
{
    Task<Result<AdvertResponse>> GetAdvertByIdAsync(int id, CancellationToken cancellationToken); 
    Task<Result<AdvertPreviewResponse>> GetAdvertPreviewByIdAsync(int id, CancellationToken cancellationToken);
}