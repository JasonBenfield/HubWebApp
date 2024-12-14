using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceRoleAccessRequest
{
    public GetResourceRoleAccessRequest()
        : this(AppVersionKey.None, 0)
    {
    }

    public GetResourceRoleAccessRequest(AppVersionKey versionKey, int resourceID)
    {
        VersionKey = versionKey.DisplayText;
        ResourceID = resourceID;
    }

    public string VersionKey { get; set; }
    public int ResourceID { get; set; }
}
