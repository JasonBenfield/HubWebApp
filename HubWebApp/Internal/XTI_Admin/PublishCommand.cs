namespace XTI_Admin;

internal sealed class PublishCommand : ICommand
{
    private readonly SlnFolder slnFolder;
    private readonly BuildProcess buildProcess;
    private readonly PublishProcess publishProcess;

    public PublishCommand(SlnFolder slnFolder, BuildProcess buildProcess, PublishProcess publishProcess)
    {
        this.slnFolder = slnFolder;
        this.buildProcess = buildProcess;
        this.publishProcess = publishProcess;
    }

    public async Task Execute()
    {
        var appKeys = slnFolder.AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        await buildProcess.Run();
        await publishProcess.Run();
    }
}