using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class BranchVersion
{
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly IHubAdministration hubAdmin;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public BranchVersion(IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo, ProductionHubAdmin prodHubAdmin, AppVersionNameAccessor versionNameAccessor)
    {
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
        hubAdmin = prodHubAdmin.Value;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task<XtiVersionModel> Value(CancellationToken ct)
    {
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        AppVersionKey versionKey;
        if (xtiBranchName is XtiIssueBranchName issueBranchName)
        {
            var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
            var milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
            versionKey = AppVersionKey.Parse(milestoneName.Version.Key);
        }
        else if (xtiBranchName is XtiVersionBranchName versionBranchName)
        {
            versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
        }
        else
        {
            versionKey = AppVersionKey.Current;
        }
        var version = await hubAdmin.Version(versionNameAccessor.Value, versionKey, ct);
        return version;
    }
}