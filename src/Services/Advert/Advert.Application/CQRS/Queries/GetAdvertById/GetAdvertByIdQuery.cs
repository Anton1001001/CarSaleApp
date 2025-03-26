using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertById;

public record GetAdvertByIdQuery(int Id) : IRequest<Result<AdvertResponse>>;

