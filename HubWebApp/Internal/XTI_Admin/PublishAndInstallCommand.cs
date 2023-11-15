using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class PublishAndInstallCommand : ICommand
{
    private readonly SlnFolder slnFolder;
    private readonly BuildProcess buildProcess;
    private readonly PublishProcess publishProcess;
    private readonly InstallProcess installProcess;

    public PublishAndInstallCommand(SlnFolder slnFolder, BuildProcess buildProcess, PublishProcess publishProcess, InstallProcess installProcess)
    {
        this.slnFolder = slnFolder;
        this.buildProcess = buildProcess;
        this.publishProcess = publishProcess;
        this.installProcess = installProcess;
    }

    public async Task Execute()
    {
        var appKeys = slnFolder.AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        await buildProcess.Run();
        await publishProcess.Run();
        await installProcess.Run();
    }
}