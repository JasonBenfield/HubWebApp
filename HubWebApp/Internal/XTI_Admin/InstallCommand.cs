namespace XTI_Admin;

internal sealed class InstallCommand : ICommand
{
    private readonly SlnFolder slnFolder;
    private readonly InstallProcess installProcess;

    public InstallCommand(SlnFolder slnFolder, InstallProcess installProcess)
    {
        this.slnFolder = slnFolder;
        this.installProcess = installProcess;
    }

    public Task Execute()
    {
        var appKeys = slnFolder.AppKeys();
        var joinedAppKeys = string.Join(",", appKeys.Select(a => a.Serialize()));
        Console.WriteLine($"App Keys: {joinedAppKeys}");
        return installProcess.Run();
    }
}