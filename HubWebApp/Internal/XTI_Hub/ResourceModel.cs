using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class ResourceModel
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public bool IsAnonymousAllowed { get; set; }
    public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.None;
}