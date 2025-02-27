using Advert.Application.Common.Advert.Models;
using MediatR;

namespace Advert.Application.CQRS.Commands.PublishAdvert;

public record PublishAdvertCommand(int Id) : IRequest<AdvertResponse>;