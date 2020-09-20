using HubWebApp.AuthApi;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebApp.Fakes
{
    public sealed class FakeAccessToken : AccessToken
    {
        public FakeAccessToken()
        {
            Token = Guid.NewGuid().ToString("N");
        }

        public string Token { get; }
        public IEnumerable<Claim> Claims { get; private set; }

        public Task<string> Generate(IEnumerable<Claim> claims)
        {
            Claims = claims;
            return Task.FromResult(Token);
        }
    }
}
