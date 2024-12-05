using XTI_HubAppClient;
using XTI_TempLog.Abstractions;

namespace XTI_PermanentLog.Implementations;

public sealed class HcPermanentLog : IPermanentLog
{
    private readonly HubAppClient client;

    public HcPermanentLog(HubAppClient client)
    {
        this.client = client;
    }

    public Task LogBatch(LogBatchModel batch, CancellationToken ct) => 
        client.PermanentLog.LogBatch(batch, ct);

    public Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct) =>
        client.PermanentLog.LogSessionDetails(new(sessionDetails), ct);
}
