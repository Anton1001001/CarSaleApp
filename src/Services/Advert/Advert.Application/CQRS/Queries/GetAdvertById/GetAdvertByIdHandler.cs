using Advert.Application.Common.Advert.Models;
using Advert.Application.Errors;
using Advert.Application.Services.Interfaces;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertById;

public class GetAdvertByIdHandler(
    IAdvertService advertService)
    : IRequestHandler<GetAdvertByIdQuery, Result<AdvertResponse>>
{
    public async Task<Result<AdvertResponse>> Handle(GetAdvertByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await advertService.GetAdvertByIdAsync(request.Id, cancellationToken);
        
        return response;
    }
}