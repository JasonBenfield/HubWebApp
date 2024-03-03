using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

internal sealed class NewIssueCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;

    public NewIssueCommand(AdminOptions options, IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo)
    {
        this.options = options;
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
    }

    public async Task Execute(CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(options.IssueTitle)) { throw new ArgumentException("Issue Title is required"); }
        var currentBranchName = gitRepo.CurrentBranchName();
        XtiGitVersion xtiGitVersion;
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is XtiIssueBranchName issueBranchName)
        {
            if (options.StartIssue)
            {
                throw new ArgumentException("Unable to start issue when not a version branch");
            }
            var branchIssue = await gitHubRepo.Issue(issueBranchName.IssueNumber);
            var xtiMilestoneName = XtiMilestoneName.Parse(branchIssue.Milestone.Title);
            xtiGitVersion = xtiMilestoneName.Version;
        }
        else if (xtiBranchName is XtiVersionBranchName versionBranchName)
        {
            xtiGitVersion = versionBranchName.Version;
        }
        else
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not an issue branch or a version branch");
        }
        var issue = await gitHubRepo.CreateIssue(xtiGitVersion, options.IssueTitle);
        if (options.StartIssue)
        {
            await gitHubRepo.StartIssue(xtiGitVersion, issue.Number);
            await gitRepo.CheckoutBranch(issue.BranchName().Value);
        }
    }
}