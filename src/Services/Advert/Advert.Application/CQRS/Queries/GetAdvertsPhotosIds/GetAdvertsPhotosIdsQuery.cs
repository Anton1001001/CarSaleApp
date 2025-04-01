using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertsPhotosIds;

public record GetAdvertsPhotosIdsQuery : IRequest<GetAdvertsPhotosIdsResponse>;