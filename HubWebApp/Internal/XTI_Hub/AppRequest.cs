using XTI_Core;
using XTI_Hub.Abstractions;
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
        this.record = record;
        ID = this.record.ID;
    }

    internal int ID { get; }

    public bool HasEnded() => record.TimeEnded < DateTimeOffset.MaxValue;

    public Task<Resource> Resource() => factory.Resources.Resource(record.ResourceID);

    public Task<Modifier> Modifier(CancellationToken ct) => factory.Modifiers.Modifier(record.ModifierID, ct);

    public Task<Installation> Installation(CancellationToken ct) =>
        factory.Installations.InstallationOrDefault(record.InstallationID, ct);

    public Task<AppSession> Session(CancellationToken ct) => factory.Sessions.Session(record.SessionID, ct);

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

    public Task<LogEntry[]> Events() => factory.LogEntries.RetrieveByRequest(this);

    public Task<AppRequest> SourceRequestOrDefault() =>
        factory.Requests.SourceRequestOrDefault(record.ID);

    public Task<int[]> TargetRequestIDs() =>
        factory.Requests.TargetRequestIDs(record.ID);

    public Task<LogEntry> LogEvent
    (
        string logEntryKey,
        AppEventSeverity severity,
        DateTimeOffset timeOccurred,
        string caption,
        string message,
        string detail,
        int actualCount,
        string sourceLogEntryKey,
        string category
    ) => factory.LogEntries.LogEvent
        (
            this,
            logEntryKey,
            timeOccurred,
            severity,
            caption,
            message,
            detail,
            actualCount,
            sourceLogEntryKey,
            category
        );

    public Task End(DateTimeOffset timeEnded, CancellationToken ct)
        => factory.DB
            .Requests
            .Update
            (
                record,
                r =>
                {
                    r.TimeEnded = timeEnded;
                },
                ct
            );

    public string RequestData { get => record.RequestData; }
    public string ResultData { get => record.ResultData; }

    public AppRequestModel ToModel() =>
        new
        (
            ID: ID,
            Path: record.Path,
            ResourceID: record.ResourceID,
            ModifierID: record.ModifierID,
            TimeStarted: record.TimeStarted,
            TimeEnded: record.TimeEnded,
            ActualCount: record.ActualCount
        );

    public override string ToString() => $"{nameof(AppRequest)} {ID}";
}