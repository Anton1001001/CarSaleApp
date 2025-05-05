using Microsoft.AspNetCore.Identity;

namespace User.Core.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}