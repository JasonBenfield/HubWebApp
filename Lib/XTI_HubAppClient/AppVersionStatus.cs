// Generated Code
namespace XTI_HubAppClient;
public sealed class AppVersionStatus : NumericValue
{
    public sealed class AppVersionStatuss : NumericValues<AppVersionStatus>
    {
        internal AppVersionStatuss()
        {
            NotSet = Add(new AppVersionStatus(0, "Not Set"));
            New = Add(new AppVersionStatus(1, "New"));
            Publishing = Add(new AppVersionStatus(2, "Publishing"));
            Old = Add(new AppVersionStatus(3, "Old"));
            Current = Add(new AppVersionStatus(4, "Current"));
        }

        public AppVersionStatus NotSet { get; }

        public AppVersionStatus New { get; }

        public AppVersionStatus Publishing { get; }

        public AppVersionStatus Old { get; }

        public AppVersionStatus Current { get; }
    }

    public static readonly AppVersionStatuss Values = new AppVersionStatuss();
    private AppVersionStatus(int value, string displayText) : base(value, displayText)
    {
    }
}