using User.Core.CQRS.Queries.GetCurrentUser;
using User.Core.Models;

namespace User.Tests.CQRS.Queries.GetCurrentUser;

public static class TestDataFactory
{
    public static string CorrectUserId = "test";
    public static string IncorrectUserId = "incorrect";
    public static GetCurrentUserQuery CreateGetCurrentUserQuery(string userId = "test")
    {
        return new GetCurrentUserQuery(userId);
    }

    
    public static ApplicationUser CreateTestUser(string userId = "test")
    {
        return new ApplicationUser
        {
            Id = userId,
            Email = "test@example.com",
            Name = "Test User"
        };
    }

}