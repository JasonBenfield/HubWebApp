using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class NewVersionCommand : ICommand
{
    private readonly Scopes scopes;

    public NewVersionCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        var appKeys = scopes.GetRequiredService<SelectedAppKeys>().Values;
        if (!appKeys.Any())
        {
            throw new ArgumentException("App keys are required");
        }
        var gitRepo = scopes.GetRequiredService<IXtiGitRepository>();
        var currentBranchName = gitRepo.CurrentBranchName();
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        var repoInfo = await gitHubRepo.RepositoryInformation();
        if (!currentBranchName.Equals(repoInfo.DefaultBranch, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"Current branch '{currentBranchName}' is not the default branch '{repoInfo.DefaultBranch}'");
        }
        var versionType = AppVersionType.Values.Value(options.VersionType);
        if (versionType == null)
        {
            throw new ArgumentException($"Version type '{options.VersionType}' is not valid");
        }
        var hubAdministration = scopes.GetRequiredService<IHubAdministration>();
        var newVersion = await hubAdministration.StartNewVersion
        (
            new AppVersionName().Value,
            versionType,
            appKeys.Select(ak => new AppDefinitionModel(ak, ak.Type.Equals(AppType.Values.WebApp) ? options.Domain : "")).ToArray()
        );
        var gitVersion = new XtiGitVersion(versionType.DisplayText, newVersion.VersionKey.DisplayText);
        await gitHubRepo.CreateNewVersion(gitVersion);
        var newVersionBranchName = gitVersion.BranchName();
        await gitRepo.CheckoutBranch(newVersionBranchName.Value);
    }
}