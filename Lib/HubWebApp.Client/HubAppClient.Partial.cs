using XTI_WebAppClient;

namespace HubWebApp.Client
{
    partial class HubAppClient : IAuthClient
    {
        IAuthApiClientGroup IAuthClient.AuthApi { get => AuthApi; }
    }
}
