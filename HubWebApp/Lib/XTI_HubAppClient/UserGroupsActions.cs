// Generated Code
namespace XTI_HubAppClient;
public sealed partial class UserGroupsActions
{
    internal UserGroupsActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<UserGroupKey>(appClientUrl, "Index");
    }

    public AppClientGetAction<UserGroupKey> Index { get; }
}