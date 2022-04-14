using Microsoft.Extensions.DependencyInjection;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.Auth;

public sealed class AuthGroup : AppApiGroupWrapper
{
    public AuthGroup(AppApiGroup source, IServiceProvider sp)
        : base(source)
    {
        var actions = new WebAppApiActionFactory(source);
        Index = source.AddAction(actions.DefaultView());
        VerifyLogin = source.AddAction
        (
            actions.Action
            (
                nameof(VerifyLogin),
                () => sp.GetRequiredService<VerifyLoginAction>()
            )
        );
        VerifyLoginForm = source.AddAction
        (
            actions.PartialView
            (
                nameof(VerifyLoginForm),
                () => sp.GetRequiredService<VerifyLoginFormAction>()
            )
        );
        Login = source.AddAction
        (
            actions.Action
            (
                nameof(Login),
                () => new LoginModelValidation(),
                () => sp.GetRequiredService<LoginAction>()
            )
        );
        Logout = source.AddAction
        (
            actions.Action
            (
                nameof(Logout),
                () => sp.GetRequiredService<LogoutAction>()
            )
        );

    }
    public AppApiAction<EmptyRequest, WebViewResult> Index { get; }
    public AppApiAction<VerifyLoginForm, EmptyActionResult> VerifyLogin { get; }
    public AppApiAction<EmptyRequest, WebPartialViewResult> VerifyLoginForm { get; }
    public AppApiAction<LoginModel, WebRedirectResult> Login { get; }
    public AppApiAction<EmptyRequest, WebRedirectResult> Logout { get; }
}