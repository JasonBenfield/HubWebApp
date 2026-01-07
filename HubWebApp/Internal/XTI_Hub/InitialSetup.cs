namespace XTI_Hub;

public sealed class InitialSetup
{
    private readonly HubFactory hubFactory;

    public InitialSetup(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task Run(CancellationToken ct)
    {
        await hubFactory.Apps.AddUnknownIfNotFound(ct);
        var xtiUserGroup = await hubFactory.UserGroups.AddXtiIfNotExists(ct);
        await xtiUserGroup.AddAnonIfNotExists(DateTimeOffset.Now, ct);
        await hubFactory.UserGroups.AddGeneralIfNotExists(ct);
    }
}
