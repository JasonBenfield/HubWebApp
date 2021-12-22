using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppVersion : IAppVersion
{
    private readonly AppFactory factory;
    private readonly AppVersionEntity record;

    internal AppVersion(AppFactory factory, AppVersionEntity record)
    {
        this.factory = factory;
        this.record = record ?? new AppVersionEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }
    private int Major { get => record.Major; }
    private int Minor { get => record.Minor; }
    private int Patch { get => record.Patch; }

    public AppVersionKey Key() => AppVersionKey.Parse(record.VersionKey);

    public bool IsPublishing() => Status().Equals(AppVersionStatus.Values.Publishing);
    public bool IsCurrent() => Status().Equals(AppVersionStatus.Values.Current);
    public bool IsNew() => Status().Equals(AppVersionStatus.Values.New);

    public bool IsPatch() => Type().Equals(AppVersionType.Values.Patch);
    public bool IsMinor() => Type().Equals(AppVersionType.Values.Minor);
    public bool IsMajor() => Type().Equals(AppVersionType.Values.Major);

    private AppVersionStatus Status() => AppVersionStatus.Values.Value(record.Status);
    public AppVersionType Type() => AppVersionType.Values.Value(record.Type);

    public Version Version() => new Version(Major, Minor, Patch);
    public Version NextMajor() => new Version(Major + 1, 0, 0);
    public Version NextMinor() => new Version(Major, Minor + 1, 0);
    public Version NextPatch() => new Version(Major, Minor, Patch + 1);

    public Task<App> App() => factory.Apps.App(record.AppID);

    public async Task<AppVersion> Current()
    {
        var app = await App();
        var current = await app.CurrentVersion();
        return current;
    }

    public async Task Publishing()
    {
        if (!IsNew() && !IsPublishing())
        {
            throw PublishException.Publishing(Key(), Status());
        }
        var current = await Current();
        await factory.DB.Versions.Update(record, r =>
        {
            r.Status = AppVersionStatus.Values.Publishing.Value;
            var type = Type();
            Version nextVersion;
            if (type.Equals(AppVersionType.Values.Major))
            {
                nextVersion = current.NextMajor();
            }
            else if (type.Equals(AppVersionType.Values.Minor))
            {
                nextVersion = current.NextMinor();
            }
            else if (type.Equals(AppVersionType.Values.Patch))
            {
                nextVersion = current.NextPatch();
            }
            else
            {
                throw new NotSupportedException($"Version type '{type}' is not supported");
            }
            r.Major = nextVersion.Major;
            r.Minor = nextVersion.Minor;
            r.Patch = nextVersion.Build;
        });
    }

    public async Task Published()
    {
        if (!IsPublishing())
        {
            throw PublishException.Published(Key(), Status());
        }
        var app = await App();
        var current = await app.CurrentVersion();
        if (current.IsCurrent())
        {
            await current.Archive();
        }
        await factory.DB.Versions.Update(record, r =>
        {
            r.Status = AppVersionStatus.Values.Current.Value;
        });
    }

    private Task Archive()
    {
        return factory.DB.Versions.Update(record, r =>
        {
            r.Status = AppVersionStatus.Values.Old.Value;
        });
    }

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany)
        => factory.Requests.MostRecentForVersion(this, howMany);

    public Task<AppEvent[]> MostRecentErrorEvents(int howMany)
        => factory.Events.MostRecentErrorsForVersion(this, howMany);

    public Task<ResourceGroup> AddOrUpdateResourceGroup(ResourceGroupName name, ModifierCategory modCategory) => 
        factory.Groups.AddOrUpdateResourceGroup(this, name, modCategory);

    public Task<ResourceGroup[]> ResourceGroups() => factory.Groups.Groups(this);

    async Task<IResourceGroup> IAppVersion.ResourceGroup(ResourceGroupName name) => 
        await ResourceGroupByName(name);

    public Task<ResourceGroup> ResourceGroup(int id) => 
        factory.Groups.GroupForVersion(this, id);

    public Task<ResourceGroup> ResourceGroupOrDefault(ResourceGroupName name) => 
        factory.Groups.GroupOrDefault(this, name);

    public Task<ResourceGroup> ResourceGroupByName(ResourceGroupName name) => 
        factory.Groups.GroupByName(this, name);

    public Task<Resource> Resource(int id) => 
        factory.Resources.Resource(this, id);

    public AppVersionModel ToModel() => new AppVersionModel
    {
        ID = ID.Value,
        VersionKey = Key().DisplayText,
        Major = Major,
        Minor = Minor,
        Patch = Patch,
        VersionType = Type(),
        Status = Status(),
        TimeAdded = record.TimeAdded
    };

    public override string ToString() => $"{nameof(AppVersion)} {ID.Value}: {Version()}";
}