using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AddOrUpdateVersionsRequest
{
    public AddOrUpdateVersionsRequest()
        : this([], [])
    {
    }

    public AddOrUpdateVersionsRequest(AppKey[] apps, AddVersionRequest[] versions)
    {
        AppKeys = apps.Select(a => new AppKeyRequest(a)).ToArray();
        Versions = versions;
    }

    public AppKeyRequest[] AppKeys { get; set; }
    public AddVersionRequest[] Versions { get; set; }

    public AppKey[] ToAppKeys() => AppKeys.Select(a => a.ToAppKey()).ToArray();
}
