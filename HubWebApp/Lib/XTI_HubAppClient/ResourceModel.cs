// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ResourceModel
{
    public int ID { get; set; }

    public string Name { get; set; } = "";
    public bool IsAnonymousAllowed { get; set; }

    public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.GetDefault();
}