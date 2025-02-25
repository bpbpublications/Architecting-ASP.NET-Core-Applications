using Microsoft.AspNetCore.Identity;

namespace Chapter14JWT.Shared.Models;
public class User : IdentityUser, IUser
{
    /// <inheritdoc />
    public int Id { get; set; }
    /// <inheritdoc />
    public string FirstName { get; set; }
    /// <inheritdoc />
    public string LastName { get; set; }
    /// <inheritdoc />
    public string Token { get; set; }
    /// <inheritdoc />
    public List<int> AllowedFeatures { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}
