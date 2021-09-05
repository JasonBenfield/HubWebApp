using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebAppApi
{
    public interface IAccess
    {
        Task<string> GenerateToken(IEnumerable<Claim> claims);
        Task Logout();
    }
}
