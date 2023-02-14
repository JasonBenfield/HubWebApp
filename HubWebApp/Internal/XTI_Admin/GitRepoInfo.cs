namespace XTI_Admin;

public sealed class GitRepoInfo
{
    private readonly string dirRepoOwner;
    private readonly string dirRepoName;
    private readonly AdminOptions options;

    public GitRepoInfo(AdminOptions options, string slnDir)
    {
        this.options = options;
        if (string.IsNullOrWhiteSpace(options.RepoOwner))
        {
            var dirInfo = new DirectoryInfo(slnDir);
            dirRepoName = dirInfo.Name;
            dirRepoOwner = dirInfo.Parent?.Name ?? "";
        }
        else
        {
            dirRepoOwner = "";
            dirRepoName = "";
        }
    }

    public string RepoOwner
    {
        get => string.IsNullOrWhiteSpace(options.RepoOwner) ? dirRepoOwner : options.RepoOwner;
    }

    public string RepoName
    {
        get => string.IsNullOrWhiteSpace(options.RepoName) ? dirRepoName : options.RepoName;
    }

    public string RepositoryUrl() => $"https://github.com/{RepoOwner}/{RepoName}";
}
