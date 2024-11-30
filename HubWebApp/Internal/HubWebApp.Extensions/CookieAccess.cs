using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using XTI_HubWebAppApi;

namespace HubWebApp.Extensions;

public sealed class CookieAccess : IAccess
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IAccess source;

    public CookieAccess(IHttpContextAccessor httpContextAccessor, IAccess source)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.source = source;
    }

    public async Task<string> GenerateToken(IEnumerable<Claim> claims)
    {
        var token = await source.GenerateToken(claims);
        var claimsPrincipal = new ClaimsPrincipal();
        claimsPrincipal.AddIdentity
        (
            new ClaimsIdentity
            (
                claims.Union([new Claim("token", token)]),
                "Password",
                ClaimTypes.Name,
                "Recipient"
            )
        );
        var authProps = new AuthenticationProperties();
        authProps.StoreTokens
        (
            [
                new AuthenticationToken()
                {
                    Name = "Jwt",
                    Value = token
                }
            ]
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