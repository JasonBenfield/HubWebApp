// Generated Code
namespace XTI_HubAppClient;
public sealed class GeneratedStorageKeyType : ClientNumericValue
{
    public sealed class GeneratedStorageKeyTypes : ClientNumericValues<GeneratedStorageKeyType>
    {
        internal GeneratedStorageKeyTypes()
        {
            Guid = Add(new GeneratedStorageKeyType(0, "Guid"));
            SixDigit = Add(new GeneratedStorageKeyType(6, "SixDigit"));
            TenDigit = Add(new GeneratedStorageKeyType(10, "TenDigit"));
        }

        public GeneratedStorageKeyType Guid { get; }

        public GeneratedStorageKeyType SixDigit { get; }

        public GeneratedStorageKeyType TenDigit { get; }
    }

    public static readonly GeneratedStorageKeyTypes Values = new GeneratedStorageKeyTypes();
    private GeneratedStorageKeyType(int value, string displayText) : base(value, displayText)
    {
    }
}