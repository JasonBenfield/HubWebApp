using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class InstallStatus : NumericValue, IEquatable<InstallStatus>
{
    public sealed class InstallStatuses : NumericValues<InstallStatus>
    {
        public InstallStatuses()
            : base(new InstallStatus(0, nameof(NotSet)))
        {
            NotSet = DefaultValue;
            InstallPending = Add(new InstallStatus(10, nameof(InstallPending)));
            InstallStarted = Add(new InstallStatus(20, nameof(InstallStarted)));
            Installed = Add(new InstallStatus(30, nameof(Installed)));
            DeletePending = Add(new InstallStatus(40, nameof(DeletePending)));
            DeleteStarted = Add(new InstallStatus(50, nameof(DeleteStarted)));
            Deleted = Add(new InstallStatus(60, nameof(Deleted)));
        }

        public InstallStatus NotSet { get; }
        public InstallStatus InstallPending { get; }
        public InstallStatus InstallStarted { get; }
        public InstallStatus Installed { get; }
        public InstallStatus DeletePending { get; }
        public InstallStatus DeleteStarted { get; }
        public InstallStatus Deleted { get; }
    }

    public static readonly InstallStatuses Values = new InstallStatuses();

    private InstallStatus(int value, string displayText)
        : base(value, displayText)
    {
    }

    public bool Equals(InstallStatus? other) => _Equals(other);
}