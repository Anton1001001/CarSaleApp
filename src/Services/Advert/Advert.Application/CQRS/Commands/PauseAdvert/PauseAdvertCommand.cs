using Advert.Application.Common.Advert.Models;
using MediatR;

namespace Advert.Application.CQRS.Commands.PauseAdvert;

public record PauseAdvertCommand(int Id) : IRequest<AdvertResponse>;