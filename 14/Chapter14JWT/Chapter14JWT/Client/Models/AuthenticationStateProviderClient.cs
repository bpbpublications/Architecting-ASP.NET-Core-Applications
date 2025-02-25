using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Chapter14JWT.Client.Models;

public class AuthenticationStateProviderClient : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private readonly AuthenticationState anonymous;
    private readonly IJSRuntime js;

    public AuthenticationStateProviderClient(HttpClient httpClient
        , IJSRuntime js)
    {
        this.httpClient = httpClient;
        this.js = js;
        this.anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        var token = await js.InvokeAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(token))
            return anonymous;

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
    }
    /// <summary>
    /// Notifies the user authentication.
    /// </summary>
    /// <param name="token">The token.</param>
    public void NotifyUserAuthentication(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }
    /// <summary>
    /// Notifies the user logout.
    /// </summary>
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
