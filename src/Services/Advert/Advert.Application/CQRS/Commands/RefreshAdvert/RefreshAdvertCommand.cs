using Advert.Application.Common.Advert.Models;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Commands.RefreshAdvert;

public record RefreshAdvertCommand(int Id) : IRequest<Result<AdvertResponse>>;