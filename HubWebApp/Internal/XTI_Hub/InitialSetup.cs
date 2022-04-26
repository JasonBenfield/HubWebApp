namespace XTI_Hub;

public sealed class InitialSetup
{
    private readonly AppFactory appFactory;

    public InitialSetup(AppFactory appFactory)
    {
        this.appFactory = appFactory;
    }

    public async Task Run()
    {
        await appFactory.Users.AddAnonIfNotExists(DateTimeOffset.Now);
        await appFactory.Apps.AddUnknownIfNotFound();
    }
}
