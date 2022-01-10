using XTI_Git.Abstractions;
using XTI_VersionToolApi;

namespace XTI_Version;

public sealed class StartIssueCommand : VersionCommand
{
    private readonly VersionGitFactory gitFactory;

    public StartIssueCommand(VersionGitFactory gitFactory)
    {
        this.gitFactory = gitFactory;
    }

    public async Task Execute(VersionToolOptions options)
    {
        if (options.IssueNumber <= 0) { throw new ArgumentException("Issue Number is required"); }
        var gitRepo = gitFactory.CreateGitRepo();
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiVersionBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
        }
        var gitHubRepo = gitFactory.CreateGitHubRepo();
        var issue = await gitHubRepo.Issue(options.IssueNumber);
        await gitRepo.CheckoutBranch(issue.BranchName().Value);
    }
}