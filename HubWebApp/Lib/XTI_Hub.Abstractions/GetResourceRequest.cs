using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceRequest
{
    public GetResourceRequest()
        : this(AppVersionKey.None, 0)
    {
    }

    public GetResourceRequest(AppVersionKey versionKey, int resourceID)
    {
        VersionKey = versionKey.DisplayText;
        ResourceID = resourceID;
    }

    public string VersionKey { get; set; }
    public int ResourceID { get; set; }
}
