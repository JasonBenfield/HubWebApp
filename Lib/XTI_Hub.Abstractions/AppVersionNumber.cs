namespace XTI_Hub.Abstractions;

public sealed record AppVersionNumber(int Major, int Minor, int Patch)
{
    public Version ToVersion() => new Version(Major, Minor, Patch);

    public AppVersionNumber NextPatch() => new AppVersionNumber(Major, Minor, Patch + 1);

}