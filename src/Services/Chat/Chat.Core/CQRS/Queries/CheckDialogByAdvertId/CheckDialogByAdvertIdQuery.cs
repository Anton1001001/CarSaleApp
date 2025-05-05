using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.CheckDialogByAdvertId;

public record CheckDialogByAdvertIdQuery(string? UserId, int AdvertId) : IRequest<Result<CheckDialogByAdvertIdResponse>>;