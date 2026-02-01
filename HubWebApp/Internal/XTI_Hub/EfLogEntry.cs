using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfLogEntry
{
    private readonly EfHubDB db;
    private readonly LogEntryEntity logEntry;

    internal EfLogEntry(EfHubDB db, LogEntryEntity logEntry)
    {
        this.db = db;
        this.logEntry = logEntry;
    }

    public Task<EfAppRequest> Request(CancellationToken ct) =>
        db.Requests.Request(logEntry.RequestID, ct);

    public Task<EfLogEntry> SourceLogEntryOrDefault(CancellationToken ct) =>
        db.LogEntries.SourceLogEntryOrDefault(logEntry.ID, ct);

    public Task<EfLogEntry> TargetLogEntryOrDefault(CancellationToken ct) =>
        db.LogEntries.TargetLogEntryOrDefault(logEntry.ID, ct);

    public AppLogEntryModel ToModel() =>
        new
        (
            ID: logEntry.ID,
            RequestID: logEntry.RequestID,
            TimeOccurred: logEntry.TimeOccurred,
            Severity: Severity(),
            Caption: logEntry.Caption,
            Message: logEntry.Message,
            Detail: logEntry.Detail,
            Category: logEntry.Category,
            ActualCount: logEntry.ActualCount
        );

    private AppEventSeverity Severity() => AppEventSeverity.Values.Value(logEntry.Severity);

    public override string ToString() => $"{nameof(EfLogEntry)} {logEntry.ID}";
}