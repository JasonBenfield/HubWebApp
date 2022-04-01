using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppModel
{
    public int ID { get; set; }
    public AppType Type { get; set; } = AppType.Values.NotFound;
    public string AppName { get; set; } = "";
    public string Title { get; set; } = "";
    public string Domain { get; set; } = "";
}