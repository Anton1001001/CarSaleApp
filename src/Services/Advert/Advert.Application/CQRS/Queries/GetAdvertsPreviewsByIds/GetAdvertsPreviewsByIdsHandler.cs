using Advert.Application.Common.Advert.Models;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertsPreviewsByIds;

public class GetAdvertsPreviewsByIdsHandler(IUnitOfWork unitOfWork, IAdvertService advertService)
    : IRequestHandler<GetAdvertsPreviewsByIdsQuery, Result<List<AdvertPreviewResponse>>>
{
    public async Task<Result<List<AdvertPreviewResponse>>> Handle(GetAdvertsPreviewsByIdsQuery request,
        CancellationToken cancellationToken)
    {
        var response = new List<AdvertPreviewResponse>();
        foreach (var id in request.Ids)
        {
            var advertPreview = await advertService.GetAdvertPreviewByIdAsync(id, cancellationToken);
            response.Add(advertPreview.Value);
        }

        return response;
    }
}