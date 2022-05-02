namespace XTI_Hub;

public sealed class InitialSetup
{
    private readonly HubFactory appFactory;

    public InitialSetup(HubFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task Run()
    {
        await appFactory.Users.AddAnonIfNotExists(DateTimeOffset.Now);
        await appFactory.Apps.AddUnknownIfNotFound();
    }
}
