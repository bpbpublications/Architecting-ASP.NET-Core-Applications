using System.Net;

namespace Chapter14JWT.Shared.Models;

public class AuthenticationResponseDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool IsAuthenticationSuccessful { get; set; }
    public string ErrorMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}