using XTI_App.Api;
using XTI_HubAppApi.Auth;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private AuthGroup? auth;
    private AuthApiGroup? authApi;

    public AuthGroup Auth { get => auth ?? throw new ArgumentNullException(nameof(auth)); }
    public AuthApiGroup AuthApi { get => authApi ?? throw new ArgumentNullException(nameof(authApi)); }

    partial void createAuth(IServiceProvider services)
    {
        auth = new AuthGroup
        (
            source.AddGroup(nameof(Auth), ResourceAccess.AllowAnonymous()),
            services
        );
        authApi = new AuthApiGroup
        (
            source.AddGroup(nameof(AuthApi), ResourceAccess.AllowAnonymous()),
            services
        );
    }
}