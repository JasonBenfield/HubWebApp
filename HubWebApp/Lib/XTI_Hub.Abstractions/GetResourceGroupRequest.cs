using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceGroupRequest
{
    public GetResourceGroupRequest()
        : this(AppVersionKey.None, 0)
    {
    }

    public GetResourceGroupRequest(AppVersionKey versionKey, int groupID)
    {
        VersionKey = versionKey.DisplayText;
        GroupID = groupID;
    }

    public string VersionKey { get; set; }
    public int GroupID { get; set; }
}
