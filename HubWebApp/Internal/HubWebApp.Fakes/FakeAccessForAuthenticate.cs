using System.Security.Claims;
using XTI_HubWebAppApi;

namespace HubWebApp.Fakes;

public sealed class FakeAccessForAuthenticate : AccessForAuthenticate
{
    public FakeAccessForAuthenticate()
    {
        Token = Guid.NewGuid().ToString("N");
    }

    public string Token { get; }
    public IEnumerable<Claim> Claims { get; private set; } = Enumerable.Empty<Claim>();

    protected override Task<string> _GenerateToken(IEnumerable<Claim> claims)
    {
        Claims = claims;
        return Task.FromResult(Token);
    }
}