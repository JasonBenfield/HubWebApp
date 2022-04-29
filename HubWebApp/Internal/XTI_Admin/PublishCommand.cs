namespace XTI_Admin;

internal sealed class PublishCommand : ICommand
{
    private readonly Scopes scopes;

    public PublishCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var appKeys = scopes.GetRequiredService<SlnFolder>().AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        await new BuildProcess(scopes).Run();
        await new PublishProcess(scopes).Run();
    }
}