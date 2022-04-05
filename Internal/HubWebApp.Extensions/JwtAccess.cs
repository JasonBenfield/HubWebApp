using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XTI_HubAppApi;
using XTI_WebApp.Extensions;

namespace HubWebApp.Extensions;

public sealed class JwtAccess : AccessForAuthenticate
{
    private readonly XtiAuthenticationOptions xtiAuthOptions;

    public JwtAccess(XtiAuthenticationOptions xtiAuthOptions)
    {
        this.xtiAuthOptions = xtiAuthOptions;
    }

    protected override Task<string> _GenerateToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(xtiAuthOptions.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                claims
            ),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        return Task.FromResult(accessToken);
    }

    protected override Task _Logout() => Task.CompletedTask;
}