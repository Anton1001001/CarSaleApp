using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.PrintAdvert;

public record PrintAdvertQuery(int Id) : IRequest<Result<PrintAdvertResponse>>;