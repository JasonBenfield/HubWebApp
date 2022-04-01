using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class CompleteVersionProcess
{
    private readonly Scopes scopes;
    private readonly AppKey appKey;

    public CompleteVersionProcess(Scopes scopes, AppKey appKey)
    {
        this.scopes = scopes;
        this.appKey = appKey;
    }

    public async Task Run()
    {
        var gitRepo = scopes.GetRequiredService<IXtiGitRepository>();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiVersionBranchName versionBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
        }
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        await gitRepo.CommitChanges($"Version {versionBranchName.Version.Key}");
        await gitHubRepo.CompleteVersion(versionBranchName);
        var repoInfo = await gitHubRepo.RepositoryInformation();
        await gitRepo.CheckoutBranch(repoInfo.DefaultBranch);
        var hubAdmin = scopes.GetRequiredService<IHubAdministration>();
        var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
        await hubAdmin.EndPublish
        (
            new AppVersionName().Value, 
            versionKey
        );
        gitRepo.DeleteBranch(versionBranchName.Value);
    }
}