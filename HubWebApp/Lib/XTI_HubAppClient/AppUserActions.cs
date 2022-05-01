// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppUserActions
{
    internal AppUserActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<int>(appClientUrl, "Index");
    }

    public AppClientGetAction<int> Index { get; }
}