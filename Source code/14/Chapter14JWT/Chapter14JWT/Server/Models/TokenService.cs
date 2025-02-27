using Chapter14JWT.Client.Models;
using Chapter14JWT.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using System.Linq;

namespace Chapter14JWT.Server.Models;

public class TokenService : ITokenService
{
    
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection jwtSettings;
    private readonly UserManager<User> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="loginDatacontext">The login datacontext.</param>
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        jwtSettings = _configuration.GetSection("JwtSettings");

    }


    /// <summary>
    /// Generates the token options.
    /// </summary>
    /// <param name="signingCredentials">The signing credentials.</param>
    /// <param name="claims">The claims.</param>
    /// <returns></returns>
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.GetSection("validIssuer").Value,
            audience: jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }

    /// <summary>
    /// Parses the base64 without padding.
    /// </summary>
    /// <param name="base64">The base64.</param>
    /// <returns></returns>
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    /// <summary>
    /// Generates the json web token.
    /// </summary>
    /// <param name="userInfo">The user information.</param>
    /// <returns></returns>
    private string GenerateJSONWebToken(UserForAuthenticationDto userInfo, int idRole)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Chapter14  -  JWT Token example."));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "guest"),
            };

        var token = new JwtSecurityToken("Ch14",
                    "Ch14",
                    claims,
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }



    /// <inheritdoc cref="ITokenService"/>
    public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var s = string.Empty;
        foreach (var item in jsonBytes)
        {
            s += (char)item;
        }

        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
            }

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }
    /// <inheritdoc cref="ITokenService"/>
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    /// <inheritdoc cref="ITokenService"/>
    public SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    /// <inheritdoc cref="ITokenService"/>
    public User VerifyUserIdentity(UserForAuthenticationDto userFromBrowser)
    {
        var tokenDominat = string.Empty;
        List<Claim> claims = new List<Claim>();
        JwtSecurityToken tokenOptions;
        string token = string.Empty;
        User user = new();

        // Check passwords
        var hash = new HashCalculator(userFromBrowser.Password).Calculate();

        // TODO: Check the hash is equal from the Database

        // here the password is valid
        tokenDominat = GenerateJSONWebToken(userFromBrowser, 0);

        user = new User()
        {
            UserName = userFromBrowser.Username,
            Email = userFromBrowser.Username
        };
        var signingCredentials = GetSigningCredentials();

        // TODO: Get list of claims from DB
        //var cl = loginDatacontext.dataContext.RoleClaims.Where(x => x.IdRole == 0);
        //claims = cl.Select(x => new Claim(ClaimTypes.Role, x.Claim.ClaimName)).ToList();


        tokenOptions = GenerateTokenOptions(signingCredentials, claims.ToList());
        token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        user.Token = tokenDominat + "." + token;
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        user.AccessFailedCount = 0;
        user.AllowedFeatures = new List<int>();


        return user;
    }

}

