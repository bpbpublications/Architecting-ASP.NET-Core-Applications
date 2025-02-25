using Chapter14JWT.Client.Models;
using Chapter14JWT.Shared.Models;
using System.Security.Claims;

namespace Chapter14JWT.Client.Services;

public interface IAuthenticationService
{
    /// <summary>
    /// Logins the specified user for authentication.
    /// </summary>
    /// <param name="userForAuthentication">The user for authentication.</param>
    /// <returns></returns>
    Task<AuthenticationResponseDto> Login(UserForAuthenticationDto userForAuthentication);
    /// <summary>
    /// Logouts this instance.
    /// </summary>
    /// <returns></returns>
    Task Logout();
    /// <summary>
    /// Parses the claims from JWT.
    /// </summary>
    /// <param name="jwt">The JWT.</param>
    /// <returns></returns>
    IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
}