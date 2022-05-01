using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppRequest
{
    private readonly HubFactory factory;
    private readonly AppRequestEntity record;

    internal AppRequest
    (
        HubFactory factory,
        AppRequestEntity record
    )
    {
        this.factory = factory;
        this.record = record ?? new AppRequestEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public bool HasEnded() => record.TimeEnded < DateTimeOffset.MaxValue;

    public bool HappendOnOrBefore(DateTimeOffset before)
    {
        DateTimeOffset date;
        if (HasEnded())
        {
            date = record.TimeEnded;
        }
        else
        {
            date = record.TimeStarted;
        }
        return date <= before;
    }

    public Task<AppEvent[]> Events() => factory.Events.RetrieveByRequest(this);

    public Task<AppEvent> LogEvent
    (
        string eventKey,
        AppEventSeverity severity,
        DateTimeOffset timeOccurred,
        string caption,
        string message,
        string detail,
        int actualCount
    ) => factory.Events.LogEvent
        (
            this, eventKey, timeOccurred, severity, caption, message, detail, actualCount
        );

    public Task End(DateTimeOffset timeEnded)
        => factory.DB
            .Requests
            .Update(record, r =>
            {
                r.TimeEnded = timeEnded;
            });

    public AppRequestModel ToModel() => new AppRequestModel
    {
        ID = ID,
        SessionID = record.SessionID,
        Path = record.Path,
        ResourceID = record.ResourceID,
        ModifierID = record.ModifierID,
        TimeStarted = record.TimeStarted,
        TimeEnded = record.TimeEnded
    };

    public override string ToString() => $"{nameof(AppRequest)} {ID}";
}