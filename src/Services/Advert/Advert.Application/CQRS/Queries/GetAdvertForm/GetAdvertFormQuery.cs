using Advert.Application.Common.Advert.Models.Parameters;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertForm;

public record GetAdvertFormQuery(ParametersBase Parameters) : IRequest<Result<GetAdvertFormResponse>>;
