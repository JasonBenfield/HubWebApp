namespace XTI_Hub.Abstractions;

public sealed record AppCommandName(string Value) : IEquatable<string>
{
    public static AppCommandName Install = new(nameof(Install));

    public static AppCommandName Delete = new(nameof(Delete));

    public AppCommandName()
        : this("")
    {
    }

    public bool Equals(string? other) => Value.Equals(other ?? "", StringComparison.OrdinalIgnoreCase);
}
