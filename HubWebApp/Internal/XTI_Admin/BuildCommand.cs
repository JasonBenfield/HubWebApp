namespace XTI_Admin;

internal sealed class BuildCommand : ICommand
{
    private readonly Scopes scopes;

    public BuildCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public Task Execute() => new BuildProcess(scopes).Run();
}