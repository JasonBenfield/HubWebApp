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
        await new BuildProcess(scopes).Run();
        await new PublishProcess(scopes).Run();
    }
}