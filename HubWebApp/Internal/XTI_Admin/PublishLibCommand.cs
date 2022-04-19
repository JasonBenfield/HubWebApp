using XTI_Core;
using XTI_Git;
using XTI_GitHub;

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
        var currentVersion = await new CurrentVersion(scopes).Value();
        if (xtiEnv.IsProduction())
        {
            var gitRepo = scopes.GetRequiredService<IXtiGitRepository>();
            var currentBranchName = gitRepo.CurrentBranchName();
            var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
            var repoInfo = await gitHubRepo.RepositoryInformation();
            if (!currentBranchName.Equals(repoInfo.DefaultBranch, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Current branch '{currentBranchName}' is not the default branch '{repoInfo.DefaultBranch}'");
            }
            semanticVersion = currentVersion.VersionNumber.Format();
        }
        else
        {
            semanticVersion = currentVersion.NextPatch().FormatAsDev();
        }
        await new PublishLibProcess(scopes).Run(semanticVersion);
    }
}
