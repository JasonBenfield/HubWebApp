using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_Hub.Abstractions;

namespace XTI_Hub;

public sealed class AppVersion : IAppVersion
{
    private readonly AppFactory factory;
    private readonly App app;
    private readonly XtiVersion version;

    internal AppVersion(AppFactory factory, App app, XtiVersion version)
    {
        this.factory = factory;
        this.app = app;
        this.version = version;
    }

    public EntityID ID { get => version.ID; }

    public AppVersionKey Key() => version.Key();

    public Task<ResourceGroup> AddOrUpdateResourceGroup(ResourceGroupName name, ModifierCategory modCategory) =>
        factory.Groups.AddOrUpdateResourceGroup(app, version, name, modCategory);

    public Task<ResourceGroup[]> ResourceGroups() => factory.Groups.Groups(app, version);

    public Task<ResourceGroup> ResourceGroup(int id) =>
        factory.Groups.GroupForVersion(app, version, id);

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName name) =>
        factory.Groups.GroupOrDefault(app, version, name);

    async Task<IResourceGroup> IAppVersion.ResourceGroup(ResourceGroupName name) =>
        await ResourceGroupByName(name);

    public Task<ResourceGroup> ResourceGroupByName(ResourceGroupName name) =>
        factory.Groups.GroupByName(app, version, name);

    public Task<Resource> Resource(int id) =>
        factory.Resources.ResourceForVersion(app, version, id);

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany) =>
        factory.Requests.MostRecentForVersion(app, version, howMany);

    public Task<AppEvent[]> MostRecentErrorEvents(int howMany) =>
        factory.Events.MostRecentErrorsForVersion(app, version, howMany);

    internal Task<int> AppVersionID() => QueryAppVersionID().FirstAsync();

    internal IQueryable<int> QueryAppVersionID() => factory.Versions.QueryAppVersionID(app, version);

    public AppModel ToAppModel() => app.ToAppModel();

    public XtiVersionModel ToVersionModel() => version.ToModel();
}