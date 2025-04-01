using Advert.Domain.Interfaces.Repositories;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertsPhotosIds;

public class GetAdvertsPhotosIdsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAdvertsPhotosIdsQuery, GetAdvertsPhotosIdsResponse>
{
    public async Task<GetAdvertsPhotosIdsResponse> Handle(GetAdvertsPhotosIdsQuery request, CancellationToken cancellationToken)
    {
        var ids = await unitOfWork.AdvertRepository.GetPhotosIdsAsync(cancellationToken);
        return new GetAdvertsPhotosIdsResponse(ids);
    }
}