using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Authenticators;

public sealed class AuthenticatorsGroup : AppApiGroupWrapper
{
    public AuthenticatorsGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        RegisterAuthenticator = source.AddAction
        (
            actions.Action
            (
                nameof(RegisterAuthenticator),
                () => sp.GetRequiredService<RegisterAuthenticatorAction>()
            )
        );
        RegisterUserAuthenticator = source.AddAction
        (
            actions.Action
            (
                nameof(RegisterUserAuthenticator),
                () => sp.GetRequiredService<RegisterUserAuthenticatorAction>()
            )
        );
    }

    public AppApiAction<EmptyRequest, EmptyActionResult> RegisterAuthenticator { get; }
    public AppApiAction<RegisterUserAuthenticatorRequest, EmptyActionResult> RegisterUserAuthenticator { get; }
}