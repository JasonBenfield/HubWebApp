namespace XTI_Admin;

public sealed class GitRepoInfo
{
    public GitRepoInfo(AdminOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.RepoOwner))
        {
            var dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
            RepoName = dirInfo.Name;
            RepoOwner = dirInfo.Parent?.Name ?? "";
        }
        else
        {
            RepoOwner = options.RepoOwner;
            RepoName = options.RepoName;
        }
    }

    public string RepoOwner { get; }
    public string RepoName { get; }

    public string RepositoryUrl() => $"https://github.com/{RepoOwner}/{RepoName}";
}
