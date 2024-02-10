using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed class AppVersionNumberRequest
{
    public AppVersionNumberRequest()
        : this(0, 0, 0)
    {
    }

    public AppVersionNumberRequest(AppVersionNumber versionNumber)
        : this(versionNumber.Major, versionNumber.Minor, versionNumber.Patch)
    {
    }

    public AppVersionNumberRequest(int major, int minor, int patch)
    {
        Major = major;
        Minor = minor;
        Patch = patch;
    }

    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }

    public AppVersionNumber ToAppVersionNumber() => new AppVersionNumber(Major, Minor, Patch);
}
