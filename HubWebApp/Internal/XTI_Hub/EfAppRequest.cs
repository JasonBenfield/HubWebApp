using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfAppRequest
{
    private readonly EfHubDB db;
    private readonly AppRequestEntity request;

    internal EfAppRequest(EfHubDB db, AppRequestEntity request)
    {
        this.db = db;
        this.request = request;
    }

    internal int ID { get => request.ID; }

    public bool HasEnded() => request.TimeEnded < DateTimeOffset.MaxValue;

    public Task<EfResource> Resource(CancellationToken ct) => db.Resources.Resource(request.ResourceID, ct);

    public Task<EfModifier> Modifier(CancellationToken ct) => db.Modifiers.Modifier(request.ModifierID, ct);

    public Task<EfInstallation> Installation(CancellationToken ct) =>
        db.Installations.InstallationOrDefault(request.InstallationID, ct);

    public Task<EfAppSession> Session(CancellationToken ct) => db.Sessions.Session(request.SessionID, ct);

    public bool HappendOnOrBefore(DateTimeOffset before)
    {
        DateTimeOffset date;
        if (HasEnded())
        {
            date = request.TimeEnded;
        }
        else
        {
            date = request.TimeStarted;
        }
        return date <= before;
    }

    public Task<EfLogEntry[]> Events(CancellationToken ct) => db.LogEntries.RetrieveByRequest(this, ct);

    public Task<EfAppRequest> SourceRequestOrDefault(CancellationToken ct) =>
        db.Requests.SourceRequestOrDefault(request.ID, ct);

    public Task<int[]> TargetRequestIDs(CancellationToken ct) =>
        db.Requests.TargetRequestIDs(request.ID, ct);

    public Task<EfLogEntry> LogEvent
    (
        string logEntryKey,
        AppEventSeverity severity,
        DateTimeOffset timeOccurred,
        string caption,
        string message,
        string detail,
        int actualCount,
        string sourceLogEntryKey,
        string category,
        CancellationToken ct
    ) => db.LogEntries.LogEvent
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
            category,
            ct
        );

    public Task End(DateTimeOffset timeEnded, CancellationToken ct) =>
        db.Context.Requests.Update
        (
            request,
            r =>
            {
                r.TimeEnded = timeEnded;
            },
            ct
        );

    public string RequestData { get => request.RequestData; }
    public string ResultData { get => request.ResultData; }

    public AppRequestModel ToModel() =>
        new
        (
            ID: ID,
            Path: request.Path,
            ResourceID: request.ResourceID,
            ModifierID: request.ModifierID,
            TimeStarted: request.TimeStarted,
            TimeEnded: request.TimeEnded,
            ActualCount: request.ActualCount
        );

    public override string ToString() => $"{nameof(EfAppRequest)} {ID}";
}