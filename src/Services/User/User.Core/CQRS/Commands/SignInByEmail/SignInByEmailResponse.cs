namespace User.Core.CQRS.Commands.SignInByEmail;

public record SignInByEmailResponse(string UserId, string? Email, string Name, string AccessToken);