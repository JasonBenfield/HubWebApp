using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class StorageName : TextValue, IEquatable<StorageName>
{
    public StorageName() : this("") { }

    public StorageName(string value) : base(value.ToLower().Replace(" ", ""), value)
    {
    }

    public bool Equals(StorageName? other) => _Equals(other);
}
