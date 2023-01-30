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

    public Task<ResourceGroup> AddOrUpdateResourceGroup(ResourceGroupName name, ModifierCategory modCategory) =>
        factory.Groups.AddOrUpdateResourceGroup(App, Version, name, modCategory);

    public Task<ResourceGroup[]> ResourceGroups() => factory.Groups.Groups(App, Version);

    public Task<ResourceGroup> ResourceGroup(int id) =>
        factory.Groups.GroupForVersion(App, Version, id);

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName name) =>
        factory.Groups.GroupOrDefault(App, Version, name);

    public Task<ResourceGroup> ResourceGroupByName(ResourceGroupName name) =>
        factory.Groups.GroupByName(App, Version, name);

    public Task<Resource> Resource(int id) =>
        factory.Resources.ResourceForVersion(App, Version, id);

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany) =>
        factory.Requests.MostRecentForVersion(App, Version, howMany);

    public Task<LogEntry[]> MostRecentLoggedErrors(int howMany) =>
        factory.LogEntries.MostRecentLoggedErrorsForVersion(App, Version, howMany);

    internal Task<int> AppVersionID() => QueryAppVersionID().FirstAsync();

    internal IQueryable<int> QueryAppVersionID() => factory.Versions.QueryAppVersionID(App, Version);

    public AppModel ToAppModel() => App.ToModel();

    public XtiVersionModel ToVersionModel() => Version.ToModel();
}