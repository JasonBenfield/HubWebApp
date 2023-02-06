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
    string Detail
)
{
    public AppLogEntryModel()
        : this(0, 0, DateTimeOffset.MaxValue, AppEventSeverity.Values.GetDefault(), "", "", "")
    {
    }
}