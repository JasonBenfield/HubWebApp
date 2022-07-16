// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsActions
{
    internal AppsActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }
}