using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Core.Errors;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Core.CQRS.Queries.GetCurrentUser;

public class GetCurrentUserHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<GetCurrentUserQuery, Result<GetCurrentUserResponse>>
{
    public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        if (userId is null)
        {
            return new BadRequestError(code: "GetCurrentUser.InvalidClaims",
                message: "Claim.NameIdentifier doesn't exist");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return new UserNotFoundError(message: $"User with id: {userId} doesn't exist");
        }

        var response = new GetCurrentUserResponse(user.Id, user.Email, user.Name);

        return response;
    }
}