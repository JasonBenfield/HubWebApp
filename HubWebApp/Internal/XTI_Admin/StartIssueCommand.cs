using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class StartIssueCommand : ICommand
{
    private readonly Scopes scopes;

    public StartIssueCommand(Scopes scopes)
    {
        this.scopes = scopes;
    }

    public async Task Execute()
    {
        var options = scopes.GetRequiredService<AdminOptions>();
        if (options.IssueNumber <= 0) { throw new ArgumentException("Issue Number is required"); }
        var gitRepo = scopes.GetRequiredService<IXtiGitRepository>();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiVersionBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
        }
        var gitHubRepo = scopes.GetRequiredService<XtiGitHubRepository>();
        var issue = await gitHubRepo.Issue(options.IssueNumber);
        await gitRepo.CheckoutBranch(issue.BranchName().Value);
    }
}