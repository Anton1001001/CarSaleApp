using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.PublishAdvert;

public record PublishAdvertCommand(int Id) : IRequest<Result<AdvertResponse>>;