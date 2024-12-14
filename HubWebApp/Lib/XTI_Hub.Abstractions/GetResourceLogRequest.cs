using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class GetResourceLogRequest
{
    public GetResourceLogRequest()
        : this(AppVersionKey.None, 0, 0)
    {
    }

    public GetResourceLogRequest(AppVersionKey versionKey, int resourceID, int howMany)
    {
        VersionKey = versionKey.DisplayText;
        ResourceID = resourceID;
        HowMany = howMany;
    }

    public string VersionKey { get; set; }
    public int ResourceID { get; set; }
    public int HowMany { get; set; }
}