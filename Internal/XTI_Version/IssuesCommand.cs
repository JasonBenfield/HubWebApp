using XTI_Git.Abstractions;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class IssuesCommand : VersionCommand
{
    private readonly VersionGitFactory gitFactory;

    public IssuesCommand(VersionGitFactory gitFactory)
    {
        this.gitFactory = gitFactory;
    }

    public async Task Execute(VersionToolOptions options)
    {
        var gitRepo = gitFactory.CreateGitRepo();
        var currentBranchName = gitRepo.CurrentBranchName();
        var gitHubRepo = gitFactory.CreateGitHubRepo();
        XtiMilestoneName milestoneName;
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is XtiIssueBranchName issueBranchName)
        {
            var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
            milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
        }
        else if (xtiBranchName is XtiVersionBranchName versionBranchName)
        {
            milestoneName = versionBranchName.Version.MilestoneName();
        }
        else
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not an issue branch or a version branch");
        }
        var milestone = await gitHubRepo.Milestone(milestoneName.Value);
        var issues = await gitHubRepo.OpenIssues(milestone);
        foreach (var issue in issues)
        {
            Console.WriteLine($"{issue.Number}: {issue.Title}");
        }
    }
}