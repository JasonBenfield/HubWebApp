using XTI_App.Abstractions;
using XTI_Core;
using XTI_Git;
using XTI_GitHub;

namespace XTI_Admin;

internal sealed class PublishLibCommand : ICommand
{
    private readonly XtiEnvironment xtiEnv;
    private readonly CurrentVersion currentVersionAccessor;
    private readonly IXtiGitRepository gitRepo;
    private readonly XtiGitHubRepository gitHubRepo;
    private readonly SelectedAppKeys selectedAppKeys;
    private readonly VersionKeyFromCurrentBranch versionKeyFromCurrentBranch;
    private readonly PublishLibProcess publishLibProcess;

    public PublishLibCommand(XtiEnvironment xtiEnv, CurrentVersion currentVersionAccessor, IXtiGitRepository gitRepo, XtiGitHubRepository gitHubRepo, SelectedAppKeys selectedAppKeys, VersionKeyFromCurrentBranch versionKeyFromCurrentBranch, PublishLibProcess publishLibProcess)
    {
        this.xtiEnv = xtiEnv;
        this.currentVersionAccessor = currentVersionAccessor;
        this.gitRepo = gitRepo;
        this.gitHubRepo = gitHubRepo;
        this.selectedAppKeys = selectedAppKeys;
        this.versionKeyFromCurrentBranch = versionKeyFromCurrentBranch;
        this.publishLibProcess = publishLibProcess;
    }

    public async Task Execute(CancellationToken ct)
    {
        string semanticVersion;
        var currentVersion = await currentVersionAccessor.Value(ct);
        if (xtiEnv.IsProduction())
        {
            var currentBranchName = gitRepo.CurrentBranchName();
            var repoInfo = await gitHubRepo.RepositoryInformation();
            if (!currentBranchName.Equals(repoInfo.DefaultBranch, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"Current branch '{currentBranchName}' is not the default branch '{repoInfo.DefaultBranch}'");
            }
            semanticVersion = currentVersion.VersionNumber.Format();
        }
        else
        {
            semanticVersion = currentVersion.NextPatch().FormatAsDev();
        }
        var versionKey = xtiEnv.IsProduction() ? 
            versionKeyFromCurrentBranch.Value() : 
            AppVersionKey.Current;
        var slnDir = Environment.CurrentDirectory;
        var appKeys = selectedAppKeys.Values;
        foreach(var appKey in appKeys)
        {
            var projectDir = Path.Combine(slnDir, new AppDirectoryName(appKey).Value);
            if (Directory.Exists(projectDir))
            {
                Environment.CurrentDirectory = projectDir;
            }
            await publishLibProcess.Run(appKey, versionKey, semanticVersion);
            Environment.CurrentDirectory = slnDir;
        }
    }
}
