namespace XTI_HubAppClient;

public sealed class PermanentLogClient : IPermanentLogClient
{
    private readonly HubAppClient client;

    public PermanentLogClient(HubAppClient client)
    {
        this.client = client;
    }

    public Task LogBatch(LogBatchModel model) => client.PermanentLog.LogBatch(model);
}
