using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceGroupRoleAccessRequest
{
    public GetResourceGroupRoleAccessRequest()
        : this(AppVersionKey.None, 0)
    {
    }

    public GetResourceGroupRoleAccessRequest(AppVersionKey versionKey, int groupID)
    {
        VersionKey = versionKey.DisplayText;
        GroupID = groupID;
    }

    public string VersionKey { get; set; }
    public int GroupID { get; set; }
}
