// Generated Code
using XTI_WebAppClient;

namespace XTI_HubAppClient
{
    public sealed class AppVersionType : NumericValue
    {
        public sealed class AppVersionTypes : NumericValues<AppVersionType>
        {
            internal AppVersionTypes()
            {
                NotSet = Add(new AppVersionType(0, "Not Set"));
                Major = Add(new AppVersionType(1, "Major"));
                Minor = Add(new AppVersionType(2, "Minor"));
                Patch = Add(new AppVersionType(3, "Patch"));
            }

            public AppVersionType NotSet { get; }

            public AppVersionType Major { get; }

            public AppVersionType Minor { get; }

            public AppVersionType Patch { get; }
        }

        public static readonly AppVersionTypes Values = new AppVersionTypes();
        public AppVersionType(int value, string displayText): base(value, displayText)
        {
        }
    }
}