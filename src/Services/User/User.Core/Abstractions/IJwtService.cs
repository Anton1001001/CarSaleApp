using User.Core.Models;

namespace User.Core.Abstractions;

public interface IJwtService
{
    string CreateRefreshToken();
    string CreateAccessToken(ApplicationUser user);
}