using System.Security.Claims;

namespace XTI_HubAppApi;

public interface IAccess
{
    Task<string> GenerateToken(IEnumerable<Claim> claims);
    Task Logout();
}