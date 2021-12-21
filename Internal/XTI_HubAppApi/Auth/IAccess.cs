using System.Security.Claims;

namespace XTI_HubAppApi.Auth;

public interface IAccess
{
    Task<string> GenerateToken(IEnumerable<Claim> claims);
    Task Logout();
}