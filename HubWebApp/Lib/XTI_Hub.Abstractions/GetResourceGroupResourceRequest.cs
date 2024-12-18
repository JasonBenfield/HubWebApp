using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceGroupResourceRequest
{
    public GetResourceGroupResourceRequest()
        : this(AppVersionKey.None, 0, new ResourceName(""))
    {
    }

    public GetResourceGroupResourceRequest(AppVersionKey versionKey, int groupID, ResourceName resourceName)
    {
        VersionKey = versionKey.DisplayText;
        GroupID = groupID;
        ResourceName = resourceName.DisplayText;
    }

    public string VersionKey { get; set; }
    public int GroupID { get; set; }
    public string ResourceName { get; set; }
}
