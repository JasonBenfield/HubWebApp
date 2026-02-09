using XTI_Core;
using XTI_GitHub;
using XTI_Internal.Implementations;

namespace XTI_HubWebAppApiActions.App;

public sealed class UpdateVersionsFromPublishedAction : AppAction<EmptyRequest, EmptyActionResult>
{
    private readonly AppFromPath appFromPath;
    private readonly IGitHubFactory gitHubFactory;
    private readonly EfHubDB db;
    private readonly IClock clock;

    public UpdateVersionsFromPublishedAction(AppFromPath appFromPath, IGitHubFactory gitHubFactory, EfHubDB db, IClock clock)
    {
        this.appFromPath = appFromPath;
        this.gitHubFactory = gitHubFactory;
        this.db = db;
        this.clock = clock;
    }

    public async Task<EmptyActionResult> Execute(EmptyRequest requestData, CancellationToken stoppingToken)
    {
        var efApp = await appFromPath.Value(stoppingToken);
        var app = efApp.ToModel();
        if (string.IsNullOrWhiteSpace(app.RepoOwner))
        {
            throw new AppException("Repo Owner is required.");
        }
        if (string.IsNullOrWhiteSpace(app.RepoName))
        {
            throw new AppException("Repo Name is required.");
        }
        using var publishedAssets = CreatePublishedAssets(app.RepoOwner, app.RepoName);
        var versionsPath = await publishedAssets.LoadVersions();
        var versionReader = new VersionReader(versionsPath);
        var publishedVersions = await versionReader.Versions();
        foreach (var publishedVersion in publishedVersions)
        {
            var addRequest = new AddVersionRequest(publishedVersion);
            var efVersion = await db.Versions.AddIfNotFound
            (
                addRequest.ToAppVersionName(),
                addRequest.ToAppVersionKey(),
                clock.Now(),
                addRequest.ToAppVersionStatus(),
                addRequest.ToAppVersionType(),
                addRequest.VersionNumber.ToAppVersionNumber(),
                stoppingToken
            );
            await efApp.AddVersionIfNotFound(efVersion, stoppingToken);
        }
        return new EmptyActionResult();
    }

    private IPublishedAssets CreatePublishedAssets(string repoOwner, string repoName)
    {
        var gitHubRepo = gitHubFactory.CreateGitHubRepository(repoOwner, repoName);
        return new GitHubPublishedAssets
        (
            gitHubRepo,
            "xti_hub_installation"
        );
    }
}
