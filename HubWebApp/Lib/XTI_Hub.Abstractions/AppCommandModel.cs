namespace XTI_Hub.Abstractions;

public sealed record AppCommandModel
(
    int ID,
    AppCommandName CommandName,
    DateTimeOffset TimeAdded,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded
)
{
    public AppCommandModel()
        : this(0, new(), DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue)
    {
    }
}
