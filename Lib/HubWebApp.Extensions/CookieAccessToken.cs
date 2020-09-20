using HubWebApp.AuthApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebApp.Extensions
{
    public sealed class CookieAccessToken : AccessToken
    {
        public CookieAccessToken(IHttpContextAccessor httpContextAccessor, AccessToken source)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.source = source;
        }

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AccessToken source;

        public async Task<string> Generate(IEnumerable<Claim> claims)
        {
            var token = await source.Generate(claims);
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity
            (
                new ClaimsIdentity
                (
                    claims,
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
            await httpContextAccessor.HttpContext.SignInAsync
            (
                claimsPrincipal,
                authProps
            );
            return token;
        }
    }
}
