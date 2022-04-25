namespace XTI_Admin;

internal sealed class LocalInstallCommand : ICommand
{
    private readonly Scopes scopes;

    public LocalInstallCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var appKeys = scopes.GetRequiredService<SelectedAppKeys>().Values;
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        foreach (var appKey in appKeys)
        {
            await new LocalInstallProcess(scopes, appKey, publishedAssets).Run();
        }
    }
}
