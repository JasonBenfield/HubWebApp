using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.ExternalAuth;

public sealed class ExternalAuthGroup : AppApiGroupWrapper
{
    public ExternalAuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Login = source.AddAction
        (
            actions.Action
            (
                nameof(Login),
                () => sp.GetRequiredService<ExternalLoginAction>()
            )
        );
    }
    public AppApiAction<ExternalLoginRequest, WebRedirectResult> Login { get; }
}