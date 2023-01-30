using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AppUserGroupName : TextKeyValue, IEquatable<AppUserGroupName>
{
    public static readonly AppUserGroupName XTI = new AppUserGroupName(nameof(XTI));
    public static readonly AppUserGroupName General = new AppUserGroupName(nameof(General));

    public AppUserGroupName()
        : this("")
    {
    }

    public AppUserGroupName(string displayText) : base(displayText)
    {
    }

    public bool Equals(AppUserGroupName? other) => _Equals(other);
}
