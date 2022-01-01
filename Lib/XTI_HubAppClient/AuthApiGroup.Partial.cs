namespace XTI_HubAppClient;

partial class AuthApiGroup : IAuthApiClientGroup
{
    async Task<ILoginResult> IAuthApiClientGroup.Authenticate(LoginCredentials model) => await Authenticate(model);
}