namespace XTI_Admin;

internal sealed class BuildCommand : ICommand
{
    private readonly BuildProcess buildProcess;

    public BuildCommand(BuildProcess buildProcess)
    {
        this.buildProcess = buildProcess;
    }

    public Task Execute() => buildProcess.Run();
}