namespace XTI_Hub;

public sealed class InitialSetup
{
    private readonly HubFactory hubFactory;

    public InitialSetup(HubFactory hubFactory)
    {
        this.hubFactory = hubFactory;
    }

    public async Task Run()
    {
        await hubFactory.Apps.AddUnknownIfNotFound();
        var xtiUserGroup = await hubFactory.UserGroups.AddXtiIfNotExists();
        await xtiUserGroup.AddAnonIfNotExists(DateTimeOffset.Now);
        await hubFactory.UserGroups.AddGeneralIfNotExists();
    }
}
