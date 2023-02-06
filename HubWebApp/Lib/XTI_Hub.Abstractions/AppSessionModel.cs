namespace XTI_Hub.Abstractions;

public sealed record AppSessionModel
(
    int ID,
    DateTimeOffset TimeStarted,
    DateTimeOffset TimeEnded,
    string RemoteAddress,
    string UserAgent
)
{
    public AppSessionModel()
        :this(0,DateTimeOffset.MaxValue, DateTimeOffset.MaxValue, "", "")
    {
    }
}