using System.Security.Claims;

namespace XTI_HubAppApi;

public abstract class AccessForLogin : IAccess
{
    public Task<string> GenerateToken(IEnumerable<Claim> claims) =>
        _GenerateToken(claims);

    protected abstract Task<string> _GenerateToken(IEnumerable<Claim> claims);

    public Task Logout() => _Logout();

    protected abstract Task _Logout();
}