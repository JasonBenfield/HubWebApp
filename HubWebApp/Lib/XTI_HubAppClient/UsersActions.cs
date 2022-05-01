// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UsersActions
{
    internal UsersActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }
}