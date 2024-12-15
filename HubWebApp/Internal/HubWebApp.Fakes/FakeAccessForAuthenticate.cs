using System.Security.Claims;
using XTI_HubWebAppApiActions;

namespace HubWebApp.Fakes;

public sealed class FakeAccessForAuthenticate : IAccess
{
    public FakeAccessForAuthenticate()
    {
        Token = Guid.NewGuid().ToString("N");
    }

    public string Token { get; }
    public IEnumerable<Claim> Claims { get; private set; } = Enumerable.Empty<Claim>();

    public Task<string> GenerateToken(IEnumerable<Claim> claims)
    {
        Claims = claims;
        return Task.FromResult(Token);
    }
}