using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog;

public interface IPermanentLog
{
    Task LogBatch(LogBatchModel batch, CancellationToken ct);
    Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct);
}