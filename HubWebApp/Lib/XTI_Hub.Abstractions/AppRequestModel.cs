namespace XTI_Hub.Abstractions;

public sealed record AppRequestModel
(
    int ID,
    string Path,
    int ResourceID,
    int ModifierID,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded,
    int ActualCount
)
{
    public AppRequestModel()
        : this(0, "", 0, 0, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, 0)
    {
    }

    public bool IsFound() => ID > 0;
}