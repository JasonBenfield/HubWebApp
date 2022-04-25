using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppModel
{
    public int ID { get; set; }
    public AppKey AppKey { get; set; } = AppKey.Unknown;
    public AppVersionName VersionName { get; set; } = AppVersionName.Unknown;
    public string Title { get; set; } = "";
    public string Domain { get; set; } = "";
}