using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using XTI_HubWebAppApi;

namespace HubWebApp.Extensions;

public sealed class CookieAccess : AccessForLogin
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly AccessForAuthenticate source;

    public CookieAccess(IHttpContextAccessor httpContextAccessor, AccessForAuthenticate source)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.source = source;
    }

    protected override async Task<string> _GenerateToken(IEnumerable<Claim> claims)
    {
        var token = await source.GenerateToken(claims);
        var claimsPrincipal = new ClaimsPrincipal();
        claimsPrincipal.AddIdentity
        (
            new ClaimsIdentity
            (
                claims.Union(new[] { new Claim("token", token) }),
                "Password",
                ClaimTypes.Name,
                "Recipient"
            )
        );
        var authProps = new AuthenticationProperties();
        authProps.StoreTokens
        (
            new[]
            {
                new AuthenticationToken()
                {
                    Name = "Jwt",
                    Value = token
                }
            }
        );
        authProps.IsPersistent = true;
        var httpContext = httpContextAccessor.HttpContext;
        if(httpContext != null)
        {
            await httpContext.SignInAsync
            (
                claimsPrincipal,
                authProps
            );
        }
        return token;
    }
}