namespace XTI_Hub.Abstractions;

public sealed record AppCommandModel
(
    int ID,
    AppCommandName CommandName,
    AppCommandStatus Status,
    DateTimeOffset TimeAdded,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded
)
{
    public AppCommandModel()
        : this(0, new(), AppCommandStatus.Values.GetDefault(), DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue)
    {
    }

    public bool HasStarted() => TimeStarted.Year < 9999;

    public bool HasEnded() => TimeEnded.Year < 9999;
}
