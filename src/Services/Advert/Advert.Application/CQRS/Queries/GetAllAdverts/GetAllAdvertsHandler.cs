using Advert.Application.Common.Advert.Models;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAllAdverts;

public class GetAllAdvertsHandler(IUnitOfWork unitOfWork, IAdvertService advertService) : IRequestHandler<GetAllAdvertsQuery, Result<List<AdvertResponse>>>
{
    public async Task<Result<List<AdvertResponse>>> Handle(GetAllAdvertsQuery request, CancellationToken cancellationToken)
    {
        var adverts = await unitOfWork.Repository<Domain.Entities.Advert>().GetAllAsync(cancellationToken);
        var ids = adverts.Select(advert => advert.Id).ToList();
        var response = new List<AdvertResponse>();

        foreach (var id in ids)
        {
            var advert = await advertService.GetAdvertByIdAsync(id, cancellationToken);
            response.Add(advert.Value);
        }

        return response;
    }
}