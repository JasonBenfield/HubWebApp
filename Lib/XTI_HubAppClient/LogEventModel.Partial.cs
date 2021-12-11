using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient
{
    partial class LogEventModel : ILogEventModel
    {
        public LogEventModel(ILogEventModel source)
        {
            EventKey = source.EventKey;
            RequestKey = source.RequestKey;
            Severity = source.Severity;
            TimeOccurred = source.TimeOccurred;
            Caption = source.Caption;
            Message = source.Message;
            Detail = source.Detail;
            ActualCount = source.ActualCount;
        }
    }
}
