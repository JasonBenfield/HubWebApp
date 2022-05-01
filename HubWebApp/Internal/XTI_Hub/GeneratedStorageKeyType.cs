using XTI_Core;

namespace XTI_Hub;

public sealed class GeneratedStorageKeyType : NumericValue, IEquatable<GeneratedStorageKeyType>
{
    public sealed class GeneratedStorageKeyTypes : NumericValues<GeneratedStorageKeyType>
    {
        internal GeneratedStorageKeyTypes() : base(new GeneratedStorageKeyType(0, nameof(Guid)))
        {
            Guid = DefaultValue;
            SixDigit = Add(new GeneratedStorageKeyType(6, nameof(SixDigit)));
            TenDigit = Add(new GeneratedStorageKeyType(10, nameof(TenDigit)));
        }
        public GeneratedStorageKeyType Guid { get; }
        public GeneratedStorageKeyType SixDigit { get; }
        public GeneratedStorageKeyType TenDigit { get; }
    }

    public static readonly GeneratedStorageKeyTypes Values = new GeneratedStorageKeyTypes();

    private GeneratedStorageKeyType(int value, string displayText) : base(value, displayText)
    {
    }

    public bool Equals(GeneratedStorageKeyType? other) => _Equals(other);
}
