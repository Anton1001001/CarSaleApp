using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.PauseAdvert;

public record PauseAdvertCommand(int Id) : IRequest<Result<AdvertResponse>>;