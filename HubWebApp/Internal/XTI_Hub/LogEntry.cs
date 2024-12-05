using XTI_Core;
using XTI_Hub.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class LogEntry
{
    private readonly HubFactory hubFactory;
    private readonly LogEntryEntity record;

    internal LogEntry(HubFactory hubFactory, LogEntryEntity record)
    {
        this.hubFactory = hubFactory;
        this.record = record;
    }

    public Task<AppRequest> Request() =>
        hubFactory.Requests.Request(record.RequestID);

    public Task<LogEntry> SourceLogEntryOrDefault() =>
        hubFactory.LogEntries.SourceLogEntryOrDefault(record.ID);

    public Task<LogEntry> TargetLogEntryOrDefault() =>
        hubFactory.LogEntries.TargetLogEntryOrDefault(record.ID);

    public AppLogEntryModel ToModel() =>
        new
        (
            ID: record.ID,
            RequestID: record.RequestID,
            TimeOccurred: record.TimeOccurred,
            Severity: Severity(),
            Caption: record.Caption,
            Message: record.Message,
            Detail: record.Detail,
            Category: record.Category,
            ActualCount: record.ActualCount
        );

    private AppEventSeverity Severity() => AppEventSeverity.Values.Value(record.Severity);

    public override string ToString() => $"{nameof(LogEntry)} {record.ID}";
}