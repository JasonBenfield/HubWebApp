namespace XTI_Hub.Abstractions;

public sealed record AppRequestModel
(
    int ID,
    int SessionID,
    string Path,
    int ResourceID,
    int ModifierID,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded
)
{
    public AppRequestModel()
        : this(0, 0, "", 0, 0, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue)
    {
    }
}