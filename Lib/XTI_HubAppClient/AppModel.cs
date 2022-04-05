// Generated Code
namespace XTI_HubAppClient;
public sealed partial class AppModel
{
    public int ID { get; set; }

    public AppType Type { get; set; } = AppType.Values.GetDefault();
    public string AppName { get; set; } = "";
    public string Title { get; set; } = "";
    public string Domain { get; set; } = "";
}