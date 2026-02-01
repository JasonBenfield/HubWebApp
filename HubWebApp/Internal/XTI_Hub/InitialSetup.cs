namespace XTI_Hub;

public sealed class InitialSetup
{
    private readonly EfHubDB db;

    public InitialSetup(EfHubDB db)
    {
        this.db = db;
    }

    public async Task Run(CancellationToken ct)
    {
        await db.Transaction(() => db.Apps.AddUnknownIfNotFound(ct));
        var xtiUserGroup = await db.UserGroups.AddXtiIfNotExists(ct);
        await xtiUserGroup.AddAnonIfNotExists(DateTimeOffset.Now, ct);
        await db.UserGroups.AddGeneralIfNotExists(ct);
    }
}
