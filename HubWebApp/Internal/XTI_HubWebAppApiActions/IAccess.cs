using System.Security.Claims;

namespace XTI_HubWebAppApiActions;

public interface IAccess
{
    Task<string> GenerateToken(IEnumerable<Claim> claims);
}