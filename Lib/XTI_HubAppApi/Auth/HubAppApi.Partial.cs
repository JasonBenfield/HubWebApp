using System;
using XTI_App.Api;
using XTI_HubAppApi.Auth;

namespace XTI_HubAppApi
{
    partial class HubAppApi
    {
        partial void auth(IServiceProvider services)
        {
            Auth = new AuthGroup
            (
                source.AddGroup(nameof(Auth), ResourceAccess.AllowAnonymous()),
                new AuthActionFactory(services)
            );
            AuthApi = new AuthApiGroup
            (
                source.AddGroup(nameof(AuthApi), ResourceAccess.AllowAnonymous()),
                new AuthActionFactory(services)
            );
        }

        public AuthGroup Auth { get; private set; }
        public AuthApiGroup AuthApi { get; private set; }
    }
}
