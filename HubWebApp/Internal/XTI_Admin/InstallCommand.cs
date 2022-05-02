namespace XTI_Admin;

internal sealed class InstallCommand : ICommand
{
    private readonly Scopes scopes;

    public InstallCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public Task Execute()
    {
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        return new InstallProcess(scopes).Run();
    }
}