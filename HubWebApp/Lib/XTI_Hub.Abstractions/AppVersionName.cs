using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AppVersionName : TextKeyValue, IEquatable<AppVersionName>
{
    public static readonly AppVersionName None = new AppVersionName();
    public static readonly AppVersionName Unknown = new AppVersionName("Unknown");

    public AppVersionName() : this("") { }

    public AppVersionName(string value) : base(value)
    {
    }

    public bool Equals(AppVersionName? other) => _Equals(other);
}
