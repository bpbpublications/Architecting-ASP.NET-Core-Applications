using Chapter14JWT.Client.Models;
using Chapter14JWT.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Chapter14JWT.Server.Models;

public interface ITokenService
{
    string GenerateRefreshToken();
    IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
    User VerifyUserIdentity(UserForAuthenticationDto userForAuthentication);
    SigningCredentials GetSigningCredentials();
}
