using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceGroupLogRequest
{
    public GetResourceGroupLogRequest()
        : this(AppVersionKey.None, 0, 0)
    {
    }

    public GetResourceGroupLogRequest(AppVersionKey versionKey, int groupID, int howMany)
    {
        VersionKey = versionKey.DisplayText;
        GroupID = groupID;
        HowMany = howMany;
    }

    public string VersionKey { get; set; }
    public int GroupID { get; set; }
    public int HowMany { get; set; }
}