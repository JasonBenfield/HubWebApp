using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourcesRequest
{
    public GetResourcesRequest()
        : this(AppVersionKey.None, 0)
    {
    }

    public GetResourcesRequest(AppVersionKey versionKey, int groupID)
    {
        VersionKey = versionKey.DisplayText;
        GroupID = groupID;
    }

    public string VersionKey { get; set; }
    public int GroupID { get; set; }
}
