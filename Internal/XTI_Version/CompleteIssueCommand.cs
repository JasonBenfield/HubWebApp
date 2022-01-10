using XTI_Git.Abstractions;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class CompleteIssueCommand : VersionCommand
{
    private readonly VersionGitFactory gitFactory;

    public CompleteIssueCommand(VersionGitFactory gitFactory)
    {
        this.gitFactory = gitFactory;
    }

    public async Task Execute(VersionToolOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.RepoOwner)) { throw new ArgumentException("Repo Owner is required"); }
        if (string.IsNullOrWhiteSpace(options.RepoName)) { throw new ArgumentException("Repo Name is required"); }
        var gitRepo = gitFactory.CreateGitRepo();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiIssueBranchName issueBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not an issue branch");
        }
        var gitHubRepo = gitFactory.CreateGitHubRepo();
        var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
        await gitRepo.CommitChanges(issue.Title);
        await gitHubRepo.CompleteIssue(issueBranchName);
        var milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
        var branchName = milestoneName.Version.BranchName();
        await gitRepo.CheckoutBranch(branchName.Value);
    }
}