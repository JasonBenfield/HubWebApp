using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class CompleteIssueCommand : ICommand
{
    private readonly Scopes scopes;

    public CompleteIssueCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var gitRepoInfo = scopes.GetRequiredService<GitRepoInfo>();
        if (string.IsNullOrWhiteSpace(gitRepoInfo.RepoOwner)) { throw new ArgumentException("Repo Owner is required"); }
        if (string.IsNullOrWhiteSpace(gitRepoInfo.RepoName)) { throw new ArgumentException("Repo Name is required"); }
        var gitRepo = scopes.GetRequiredService<IXtiGitRepository>();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiIssueBranchName issueBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not an issue branch");
        }
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        var issue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
        await gitRepo.CommitChanges(issue.Title);
        await gitHubRepo.CompleteIssue(issueBranchName);
        var milestoneName = XtiMilestoneName.Parse(issue.Milestone.Title);
        var branchName = milestoneName.Version.BranchName();
        await gitRepo.CheckoutBranch(branchName.Value);
        gitRepo.DeleteBranch(issueBranchName.Value);
    }
}