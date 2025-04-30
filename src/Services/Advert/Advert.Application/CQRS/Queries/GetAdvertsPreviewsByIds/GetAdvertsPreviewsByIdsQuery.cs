using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertsPreviewsByIds;

public record GetAdvertsPreviewsByIdsQuery(List<int> Ids) : IRequest<Result<List<AdvertPreviewResponse>>>;
