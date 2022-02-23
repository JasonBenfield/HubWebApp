using XTI_Core;

namespace XTI_Admin;

internal sealed class PublishLibCommand : ICommand
{
    private readonly Scopes scopes;

    public PublishLibCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        string semanticVersion;
        var xtiEnv = scopes.GetRequiredService<XtiEnvironment>();
        if (xtiEnv.IsProduction())
        {
            var version = await new BranchVersion(scopes).Value();
            semanticVersion = $"{version.Version()}";
        }
        else
        {
            var currentVersion = await new CurrentVersion(scopes).Value();
            semanticVersion = $"{currentVersion.NextPatch()}-dev{DateTime.Now:yyMMddHHmmssfff}";
        }
        await new PublishLibProcess(scopes).Run(semanticVersion);
    }
}
