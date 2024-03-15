namespace XTI_HubAppClient;

partial class AuthApiGroup : IAuthApiClientGroup
{
    async Task<LoginResult> IAuthApiClientGroup.Authenticate(AuthenticateRequest authRequest) => await Authenticate(authRequest);
}