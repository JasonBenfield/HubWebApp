using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class GeneratedKeyType : NumericValue, IEquatable<GeneratedKeyType>
{
    public sealed class GeneratedStorageKeyTypes : NumericValues<GeneratedKeyType>
    {
        internal GeneratedStorageKeyTypes() : base(new GeneratedKeyType(0, nameof(Guid)))
        {
            Guid = DefaultValue;
            SixDigit = Add(new GeneratedKeyType(6, nameof(SixDigit)));
            TenDigit = Add(new GeneratedKeyType(10, nameof(TenDigit)));
            Fixed = Add(new GeneratedKeyType(999, nameof(Fixed)));
        }
        public GeneratedKeyType Guid { get; }
        public GeneratedKeyType SixDigit { get; }
        public GeneratedKeyType TenDigit { get; }
        public GeneratedKeyType Fixed { get; }
    }

    public static readonly GeneratedStorageKeyTypes Values = new GeneratedStorageKeyTypes();

    private GeneratedKeyType(int value, string displayText) : base(value, displayText)
    {
    }

    public bool Equals(GeneratedKeyType? other) => _Equals(other);
}
