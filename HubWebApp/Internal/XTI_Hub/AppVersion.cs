using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;

namespace XTI_Hub;

public sealed class AppVersion
{
    private readonly HubFactory factory;

    internal AppVersion(HubFactory factory, App app, XtiVersion version)
    {
        this.factory = factory;
        App = app;
        Version = version;
    }

    public App App { get; }

    public XtiVersion Version { get; }

    public Task<ResourceGroup> AddOrUpdateResourceGroup(ResourceGroupName name, ModifierCategory modCategory, CancellationToken ct) =>
        factory.Groups.AddOrUpdateResourceGroup(App, Version, name, modCategory, ct);

    public Task<ResourceGroup[]> ResourceGroups(CancellationToken ct) => factory.Groups.Groups(App, Version, ct);

    public Task<ResourceGroup> ResourceGroup(int id, CancellationToken ct) =>
        factory.Groups.GroupForVersion(App, Version, id, ct);

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName name, CancellationToken ct) =>
        factory.Groups.GroupOrDefault(App, Version, name, ct);

    public Task<ResourceGroup> ResourceGroupByName(ResourceGroupName name, CancellationToken ct) =>
        factory.Groups.GroupByName(App, Version, name, ct);

    public Task<Resource> Resource(int id, CancellationToken ct) =>
        factory.Resources.ResourceForVersion(App, Version, id, ct);

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany, CancellationToken ct) =>
        factory.Requests.MostRecentForVersion(App, Version, howMany, ct);

    public Task<LogEntry[]> MostRecentLoggedErrors(int howMany, CancellationToken ct) =>
        factory.LogEntries.MostRecentLoggedErrorsForVersion(App, Version, howMany, ct);

    internal Task<int> AppVersionID(CancellationToken ct) => QueryAppVersionID().FirstAsync(ct);

    internal IQueryable<int> QueryAppVersionID() => factory.Versions.QueryAppVersionID(App, Version);
}