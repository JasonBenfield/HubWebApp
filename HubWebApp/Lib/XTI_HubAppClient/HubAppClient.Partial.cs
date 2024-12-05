using System.Text.Json;

namespace XTI_HubAppClient;

partial class HubAppClient : IAuthClient
{
    IAuthApiClientGroup IAuthClient.AuthApi { get => AuthApi; }

}