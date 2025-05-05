using FluentResults;
using MediatR;

namespace User.Core.CQRS.Queries.GetCurrentUser;

public record GetCurrentUserQuery(string? UserId) : IRequest<Result<GetCurrentUserResponse>>;