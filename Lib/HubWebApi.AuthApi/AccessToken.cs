using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HubWebApp.AuthApi
{
    public interface AccessToken
    {
        Task<string> Generate(IEnumerable<Claim> claims);
    }
}
