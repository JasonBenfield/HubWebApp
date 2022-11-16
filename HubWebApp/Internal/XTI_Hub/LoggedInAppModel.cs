using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed record LoggedInAppModel(AppName AppName, AppVersionKey VersionKey, string Domain)
{
    public LoggedInAppModel()
        : this(AppName.Unknown, new AppVersionKey(), "")
    {
    }
}