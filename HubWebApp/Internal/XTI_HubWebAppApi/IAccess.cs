using System.Security.Claims;

namespace XTI_HubWebAppApi;

public interface IAccess
{
    Task<string> GenerateToken(IEnumerable<Claim> claims);
}