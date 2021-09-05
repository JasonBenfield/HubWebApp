using System.Threading.Tasks;
using XTI_WebAppClient;

namespace HubWebApp.Client
{
    partial class AuthApiGroup : IAuthApiClientGroup
    {
        Task<LoginResult> IAuthApiClientGroup.Authenticate(LoginCredentials model) => Authenticate(model);
    }
}
