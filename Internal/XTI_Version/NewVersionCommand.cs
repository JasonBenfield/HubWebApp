using XTI_App.Abstractions;
using XTI_Core;
using XTI_Git.Abstractions;
using XTI_Hub;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class NewVersionCommand : VersionCommand
{
    private readonly AppFactory appFactory;
    private readonly VersionGitFactory gitFactory;
    private readonly IClock clock;

    public NewVersionCommand(AppFactory appFactory, VersionGitFactory gitFactory, IClock clock)
    {
        this.appFactory = appFactory;
        this.gitFactory = gitFactory;
        this.clock = clock;
    }

    public async Task Execute(VersionToolOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
        if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
        var gitRepo = gitFactory.CreateGitRepo();
        var currentBranchName = gitRepo.CurrentBranchName();
        var gitHubRepo = gitFactory.CreateGitHubRepo();
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
        var version = await appFactory.Apps.StartNewVersion(options.AppKey(), options.Domain, versionType, clock.Now());
        var gitVersion = new XtiGitVersion(version.Type().DisplayText, version.Key().Value);
        await gitHubRepo.CreateNewVersion(gitVersion);
        var newVersionBranchName = gitVersion.BranchName();
        await gitRepo.CheckoutBranch(newVersionBranchName.Value);
    }
}