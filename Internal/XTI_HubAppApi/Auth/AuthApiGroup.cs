using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Auth;

public sealed class AuthApiGroup : AppApiGroupWrapper
{
    public AuthApiGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Authenticate = source.AddAction
        (
            actions.Action
            (
                nameof(Authenticate),
                () => new LoginValidation(),
                () => sp.GetRequiredService<AuthenticateAction>()
            )
        );
    }
    public AppApiAction<LoginCredentials, LoginResult> Authenticate { get; }
}