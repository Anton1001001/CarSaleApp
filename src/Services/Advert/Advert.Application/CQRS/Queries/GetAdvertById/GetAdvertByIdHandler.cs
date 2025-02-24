using Advert.Application.Common;
using Advert.Application.Common.Advert.Models;
using Advert.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertById;

public class GetAdvertByIdHandler(
    IUnitOfWork unitOfWork, 
    IMapper mapper,
    Processor<Domain.Entities.Advert, AdvertResponse> processor)
    : IRequestHandler<GetAdvertByIdQuery, AdvertResponse>
{
    public async Task<AdvertResponse> Handle(GetAdvertByIdQuery request, CancellationToken cancellationToken)
    {
        var advert = await unitOfWork.AdvertRepository.GetByIdAsync(request.Id, cancellationToken);
        var response = await processor.HandleAsync(advert, cancellationToken);
        return response;
    }
}