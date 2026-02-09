using XTI_Internal.Abstractions;
using XTI_TempLog.Abstractions;

namespace XTI_HubAppClient.Implementations;

public sealed class HcPermanentLog : IPermanentLog
{
    private readonly HubAppClient client;

    public HcPermanentLog(HubAppClient client)
    {
        this.client = client;
    }

    public Task LogSessionDetails(TempLogSessionDetailModel[] sessionDetails, CancellationToken ct) =>
        client.PermanentLog.LogSessionDetails(new(sessionDetails), ct);
}
