// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppActions
{
    internal AppActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }
}