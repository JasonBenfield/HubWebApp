using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class AppRequestExpandedModel
{
    public int ID { get; set; }
    public string UserName { get; set; } = "";
    public string GroupName { get; set; } = "";
    public string ActionName { get; set; } = "";
    public ResourceResultType ResultType { get; set; } = ResourceResultType.Values.None;
    public DateTimeOffset TimeStarted { get; set; }
    public DateTimeOffset TimeEnded { get; set; }
}