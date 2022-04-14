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
        await new BuildProcess(scopes).Run();
        await new PublishProcess(scopes).Run();
        await new InstallProcess(scopes).Run();
    }
}