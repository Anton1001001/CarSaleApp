using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Advert.Models.Parameters;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public record CreateAdvertCommand(string SellerId, ParametersBase Parameters) : IRequest<Result<AdvertResponse>>;
