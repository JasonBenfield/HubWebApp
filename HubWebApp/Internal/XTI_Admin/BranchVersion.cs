using Microsoft.Extensions.DependencyInjection;
using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;
using XTI_Hub.Abstractions;

namespace XTI_Admin;

internal sealed class BranchVersion
{
    private readonly IServiceProvider sp;

    public BranchVersion(Scopes scopes)
    {
        sp = scopes.Production();
    }

    public async Task<XtiVersionModel> Value()
    {
        var gitRepo = sp.GetRequiredService<IXtiGitRepository>();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        AppVersionKey versionKey;
        if (xtiBranchName is XtiIssueBranchName issueBranchName)
        {
            var gitHubRepo = sp.GetRequiredService<XtiGitHubRepository>();
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
        var hubAdmin = sp.GetRequiredService<IHubAdministration>();
        var version = await hubAdmin.Version(new AppVersionNameAccessor().Value, versionKey);
        return version;
    }
}