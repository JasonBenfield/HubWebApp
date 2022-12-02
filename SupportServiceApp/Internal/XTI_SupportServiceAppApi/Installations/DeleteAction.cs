using XTI_Hub.Abstractions;
using XTI_HubAppClient;

namespace XTI_SupportServiceAppApi.Installations;

public sealed class DeleteAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly HubAppClient hubClient;

    public DeleteAction(HubAppClient hubClient)
    {
        this.hubClient = hubClient;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest model, CancellationToken ct)
    {
        var pendingDeletes = await hubClient.Installations.GetPendingDeletes(new GetPendingDeletesRequest(Environment.MachineName), ct);
        foreach (var pendingDelete in pendingDeletes)
        {
            await hubClient.Installations.BeginDelete(new GetInstallationRequest(pendingDelete.ID), ct);
            await hubClient.Installations.Deleted(new GetInstallationRequest(pendingDelete.ID), ct);
        }
        return new EmptyActionResult();
    }
}