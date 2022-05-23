using XTI_App.Abstractions;
using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppLogEntry
{
    private readonly AppLogEntryEntity record;

    internal AppLogEntry(AppLogEntryEntity record)
    {
        this.record = record ?? new AppLogEntryEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public string Caption { get => record.Caption; }
    public string Message { get => record.Message; }
    public string Detail { get => record.Detail; }
    public AppEventSeverity Severity() => AppEventSeverity.Values.Value(record.Severity);

    public AppLogEntryModel ToModel() => new AppLogEntryModel
    {
        ID = ID,
        RequestID = record.RequestID,
        TimeOccurred = record.TimeOccurred,
        Severity = Severity(),
        Caption = Caption,
        Message = Message,
        Detail = Detail
    };

    public override string ToString() => $"{nameof(AppLogEntry)} {ID}";
}