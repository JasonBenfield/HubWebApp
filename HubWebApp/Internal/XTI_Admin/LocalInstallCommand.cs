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
        var installOptionsAccessor = scopes.GetRequiredService<InstallOptionsAccessor>();
        using var publishedAssets = scopes.GetRequiredService<IPublishedAssets>();
        foreach (var appKey in appKeys)
        {
            var installations = installOptionsAccessor.Installations(appKey);
            foreach(var installation in installations)
            {
                await new LocalInstallProcess(scopes, appKey, publishedAssets).Run(installation);
            }
        }
    }
}
