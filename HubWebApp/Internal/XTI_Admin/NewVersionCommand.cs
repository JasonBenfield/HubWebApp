using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class NewVersionCommand : ICommand
{
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly AdminOptions options;
    private readonly IHubAdministration hubAdministration;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public NewVersionCommand(IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo, AdminOptions options, IHubAdministration hubAdministration, AppVersionNameAccessor versionNameAccessor)
    {
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
        this.options = options;
        this.hubAdministration = hubAdministration;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task Execute()
    {
        var currentBranchName = gitRepo.CurrentBranchName();
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
        var versionName = versionNameAccessor.Value;
        var newVersion = await hubAdministration.StartNewVersion
        (
            versionName,
            versionType
        );
        var gitVersion = new XtiGitVersion(versionType.DisplayText, newVersion.VersionKey.DisplayText);
        await gitHubRepo.CreateNewVersion(gitVersion);
        var newVersionBranchName = gitVersion.BranchName();
        await gitRepo.CheckoutBranch(newVersionBranchName.Value);
    }
}