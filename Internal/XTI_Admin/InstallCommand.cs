namespace XTI_Admin;

internal sealed class InstallCommand : ICommand
{
    private readonly Scopes scopes;

    public InstallCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public Task Execute() => new InstallProcess(scopes).Run();
}