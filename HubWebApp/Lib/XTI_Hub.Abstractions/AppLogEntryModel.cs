using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed record AppLogEntryModel
(
    int ID,
    int RequestID,
    DateTimeOffset TimeOccurred,
    AppEventSeverity Severity,
    string Caption,
    string Message,
    string Detail,
    string Category,
    int ActualCount
)
{
    public AppLogEntryModel()
        : this(0, 0, DateTimeOffset.MaxValue, AppEventSeverity.Values.GetDefault(), "", "", "", "", 0)
    {
    }

    public bool IsFound() => ID > 0;

    public bool IsPlaceholder() => 
        Caption.Equals("Placeholder") &&
        Message.Equals("Placeholder") &&
        Detail.Equals("Placeholder");
}