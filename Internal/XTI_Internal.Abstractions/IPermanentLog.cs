using XTI_TempLog.Abstractions;

namespace XTI_Internal.Abstractions;

public interface IPermanentLog
{
    Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct);
}