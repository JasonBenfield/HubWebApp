// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersActions
{
    internal UsersActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<GetUserRequest>(appClientUrl, "Index");
    }

    public AppClientGetAction<GetUserRequest> Index { get; }
}