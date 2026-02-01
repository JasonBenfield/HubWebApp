using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class EfAppVersion
{
    private readonly EfHubDB factory;

    internal EfAppVersion(EfHubDB factory, EfApp app, EfVersion version)
    {
        this.factory = factory;
        App = app;
        Version = version;
    }

    public EfApp App { get; }

    public EfVersion Version { get; }

    public Task<EfResourceGroup> AddOrUpdateResourceGroup(ResourceGroupName name, EfModifierCategory modCategory, CancellationToken ct) =>
        factory.Groups.AddOrUpdateResourceGroup(App, Version, name, modCategory, ct);

    public Task<EfResourceGroup[]> ResourceGroups(CancellationToken ct) => factory.Groups.Groups(App, Version, ct);

    public Task<EfResourceGroup> ResourceGroup(int id, CancellationToken ct) =>
        factory.Groups.GroupForVersion(App, Version, id, ct);

    public Task<EfResourceGroup> ResourceGroupOrDefault(ResourceGroupName name, CancellationToken ct) =>
        factory.Groups.GroupOrDefault(App, Version, name, ct);

    public Task<EfResourceGroup> ResourceGroupByName(ResourceGroupName name, CancellationToken ct) =>
        factory.Groups.GroupByName(App, Version, name, ct);

    public Task<EfResource> Resource(int id, CancellationToken ct) =>
        factory.Resources.ResourceForVersion(App, Version, id, ct);

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        factory.Requests.MostRecentForVersion(App, Version, howMany, ct);

    public Task<EfLogEntry[]> MostRecentLoggedErrors(int howMany, CancellationToken ct) =>
        factory.LogEntries.MostRecentLoggedErrorsForVersion(App, Version, howMany, ct);

    internal Task<int> AppVersionID(CancellationToken ct) => QueryAppVersionID().FirstAsync(ct);

    internal IQueryable<int> QueryAppVersionID() => factory.Versions.QueryAppVersionID(App, Version);
}