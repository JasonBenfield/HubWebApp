using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog;

public interface IPermanentLog
{
    Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct);
}