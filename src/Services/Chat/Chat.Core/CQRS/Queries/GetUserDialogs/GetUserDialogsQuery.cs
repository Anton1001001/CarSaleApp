using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.GetUserDialogs;

public record GetUserDialogsQuery(string? UserId) : IRequest<Result<List<GetUserDialogsResponse>>>;