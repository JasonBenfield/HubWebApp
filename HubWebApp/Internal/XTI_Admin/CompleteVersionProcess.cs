using XTI_App.Abstractions;
using XTI_Git;
using XTI_Git.Abstractions;
using XTI_GitHub;

namespace XTI_Admin;

public sealed class CompleteVersionProcess
{
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly ProductionHubAdmin prodHubAdmin;
    private readonly AppVersionNameAccessor versionNameAccessor;

    public CompleteVersionProcess(IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo, ProductionHubAdmin prodHubAdmin, AppVersionNameAccessor versionNameAccessor)
    {
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
        this.prodHubAdmin = prodHubAdmin;
        this.versionNameAccessor = versionNameAccessor;
    }

    public async Task Run()
    {
        var currentBranchName = gitRepo.CurrentBranchName();
        var xtiBranchName = XtiBranchName.Parse(currentBranchName);
        if (xtiBranchName is not XtiVersionBranchName versionBranchName)
        {
            throw new ArgumentException($"Branch '{currentBranchName}' is not a version branch");
        }
        await gitRepo.CommitChanges($"Version {versionBranchName.Version.Key}");
        await gitHubRepo.CompleteVersion(versionBranchName);
        var repoInfo = await gitHubRepo.RepositoryInformation();
        var isDefaultCheckedOut = false;
        try
        {
            await gitRepo.CheckoutBranch(repoInfo.DefaultBranch);
            isDefaultCheckedOut = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"***Error Checking out branch {repoInfo.DefaultBranch}***\r\n{ex}");
        }
        var versionKey = AppVersionKey.Parse(versionBranchName.Version.Key);
        await prodHubAdmin.Value.EndPublish
        (
            versionNameAccessor.Value, 
            versionKey
        );
        if (isDefaultCheckedOut)
        {
            gitRepo.DeleteBranch(versionBranchName.Value);
        }
    }
}