// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppsActions
{
    internal AppsActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
        RedirectToApp = new AppClientGetAction<int>(appClientUrl, "RedirectToApp");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }

    public AppClientGetAction<int> RedirectToApp { get; }
}