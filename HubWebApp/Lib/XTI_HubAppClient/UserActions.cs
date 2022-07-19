// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserActions
{
    internal UserActions(AppClientUrl appClientUrl)
    {
        AccessDenied = new AppClientGetAction<EmptyRequest>(appClientUrl, "AccessDenied");
        Error = new AppClientGetAction<EmptyRequest>(appClientUrl, "Error");
        Logout = new AppClientGetAction<LogoutRequest>(appClientUrl, "Logout");
    }

    public AppClientGetAction<EmptyRequest> AccessDenied { get; }

    public AppClientGetAction<EmptyRequest> Error { get; }

    public AppClientGetAction<LogoutRequest> Logout { get; }
}