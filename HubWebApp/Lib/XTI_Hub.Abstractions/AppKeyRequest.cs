using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppKeyRequest
{
    public AppKeyRequest()
        : this(AppKey.Unknown)
    {
    }

    public AppKeyRequest(AppKey appKey)
    {
        AppName = appKey.Name.DisplayText;
        AppType = appKey.Type.Value;
    }

    public string AppName { get; set; }
    public int AppType { get; set; }

    public AppKey ToAppKey() => new(new AppName(AppName), XTI_App.Abstractions.AppType.Values.Value(AppType));
}
