using Chapter14JWT.Server.Models;
using Chapter14JWT.Shared.Models;
using System.Net;


namespace Chapter14JWT.Server.Controllers;

public static class ApiLoginController
{
    public static void AddLoginController(this IEndpointRouteBuilder app)
    {
        _ = app.MapPost("api/v1/login", (UserForAuthenticationDto userForAuthentication,
            ITokenService tokenService) =>
        {
            IResult ret = Results.Ok();
            var user = new User()
            {
                UserName = userForAuthentication.Username,
                Email = userForAuthentication.Username
            };

            try
            {
                // User verification
                user = tokenService.VerifyUserIdentity(userForAuthentication);
                if (user.Token != null)
                {
                    ret = Results.Ok(new AuthenticationResponseDto
                    {
                        IsAuthenticationSuccessful = true,
                        Token = user.Token,
                        RefreshToken = user.RefreshToken,
                        StatusCode = HttpStatusCode.OK
                    });
                }
                else
                {
                    ret = Results.Unauthorized();
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                s += "";
            }
            return ret;
        });
    }
}
