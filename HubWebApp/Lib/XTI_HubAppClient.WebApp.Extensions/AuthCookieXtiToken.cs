using Microsoft.AspNetCore.Http;
using XTI_WebApp.Api;
using XTI_WebAppClient;

namespace XTI_HubAppClient.WebApp.Extensions;

public sealed class AuthCookieXtiToken : IXtiToken
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly XtiClaims xtiClaims;

    public AuthCookieXtiToken(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
        xtiClaims = new XtiClaims(httpContextAccessor.HttpContext ?? new DefaultHttpContext());
    }

    public Task<string> UserName() => Task.FromResult(xtiClaims.UserName().Value);

    public Task<string> Value()
    {
        var token = httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "token")?.Value ?? 
            "";
        return Task.FromResult(token);
    }
}
