namespace User.Core.CQRS.Queries.GetCurrentUser;

public record GetCurrentUserResponse(string UserId, string? Email, string Name);