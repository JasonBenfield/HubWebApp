namespace XTI_Admin;

internal sealed class PublishAndInstallCommand : ICommand
{
    private readonly Scopes scopes;

    public PublishAndInstallCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var slnFolder = scopes.GetRequiredService<SlnFolder>();
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        await new BuildProcess(scopes).Run();
        await new PublishProcess(scopes).Run();
        Console.WriteLine("Beginning Install");
        await new InstallProcess(scopes).Run();
    }
}