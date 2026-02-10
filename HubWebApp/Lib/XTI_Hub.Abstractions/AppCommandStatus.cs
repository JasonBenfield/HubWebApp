using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class AppCommandStatus : NumericValue, IEquatable<AppCommandStatus>
{
    public sealed class AppCommandStatuses : NumericValues<AppCommandStatus>
    {
        internal AppCommandStatuses() 
            : base(new AppCommandStatus(0, nameof(NotSet)))
        {
            NotSet = DefaultValue;
            Pending = Add(new AppCommandStatus(1, nameof(Pending)));
            Started = Add(new AppCommandStatus(5, nameof(Started)));
            Completed = Add(new AppCommandStatus(10, nameof(Completed)));
            Failed = Add(new AppCommandStatus(99, nameof(Failed)));
            Cancelled = Add(new AppCommandStatus(999, nameof(Cancelled)));
        }
        public AppCommandStatus NotSet { get; }
        public AppCommandStatus Pending { get; }
        public AppCommandStatus Started { get; }
        public AppCommandStatus Completed { get; }
        public AppCommandStatus Failed { get; }
        public AppCommandStatus Cancelled { get; }
    }

    public static readonly AppCommandStatuses Values = new AppCommandStatuses();

    private AppCommandStatus(int value, string displayText) : base(value, displayText)
    {
    }

    public bool Equals(AppCommandStatus? other) => _Equals(other);
}