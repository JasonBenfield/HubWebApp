using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class InstallationQueryType : NumericValue, IEquatable<InstallationQueryType>
{
    public sealed class InstallationQueryTypes : NumericValues<InstallationQueryType>
    {
        internal InstallationQueryTypes() : base(new InstallationQueryType(0, nameof(Installed)))
        {
            Installed = DefaultValue;
            Current = Add(new(5, nameof(Current)));
            UpdateAvailable = Add(new(10, "Update Available"));
            All = Add(new(100, nameof(All)));
        }

        public InstallationQueryType Installed { get; }
        public InstallationQueryType Current { get; }
        public InstallationQueryType UpdateAvailable { get; }
        public InstallationQueryType All { get; }
    }

    public static readonly InstallationQueryTypes Values = new ();

    private InstallationQueryType(int value, string displayText) : base(value, displayText)
    {
    }

    public bool Equals(InstallationQueryType? other) => _Equals(other);
}
