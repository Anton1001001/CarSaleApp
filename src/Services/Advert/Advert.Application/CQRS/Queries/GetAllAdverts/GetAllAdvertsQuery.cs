using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAllAdverts;

public record GetAllAdvertsQuery : IRequest<Result<List<AdvertResponse>>>;
