// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsActions
{
    internal UserGroupsActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
        UserQuery = new AppClientGetAction<UserGroupKey>(appClientUrl, "UserQuery");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }

    public AppClientGetAction<UserGroupKey> UserQuery { get; }
}