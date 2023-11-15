using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class StartIssueCommand : ICommand
{
    private readonly AdminOptions options;
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;

    public StartIssueCommand(AdminOptions options, IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo)
    {
        this.options = options;
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
    }

    public async Task Execute()
    {
        if (options.IssueNumber <= 0) { throw new ArgumentException("Issue Number is required"); }
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if(xtiBranchName is XtiVersionBranchName xtiVersionBranchName)
        {
            var issue = await gitHubRepo.StartIssue(xtiVersionBranchName.Version, options.IssueNumber);
            await gitRepo.CheckoutBranch(issue.BranchName().Value);
        }
        else
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
        }
    }
}