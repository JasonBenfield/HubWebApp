// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AuthActions
{
    internal AuthActions(AppClientUrl appClientUrl)
    {
        VerifyLoginForm = new AppClientGetAction<EmptyRequest>(appClientUrl, "VerifyLoginForm");
        Login = new AppClientGetAction<LoginModel>(appClientUrl, "Login");
    }

    public AppClientGetAction<EmptyRequest> VerifyLoginForm { get; }

    public AppClientGetAction<LoginModel> Login { get; }
}