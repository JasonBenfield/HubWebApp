using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class AppVersionNameAccessor
{
    public AppVersionNameAccessor(GitRepoInfo repoInfo)
    {
        Value = new AppVersionName(repoInfo.RepoName);
    }

    public AppVersionName Value { get; }
}
