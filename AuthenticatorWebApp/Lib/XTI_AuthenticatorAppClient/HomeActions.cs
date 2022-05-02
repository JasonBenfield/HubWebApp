// Generated Code
namespace XTI_AuthenticatorAppClient;
public sealed partial class HomeActions
{
    internal HomeActions(AppClientUrl appClientUrl)
    {
        Index = new AppClientGetAction<EmptyRequest>(appClientUrl, "Index");
    }

    public AppClientGetAction<EmptyRequest> Index { get; }
}