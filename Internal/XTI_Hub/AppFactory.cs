using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class AppFactory
{
    public AppFactory(IHubDbContext db)
    {
        DB = db;
    }

    internal IHubDbContext DB { get; }

    private AppUserRepository? users;

    public AppUserRepository Users { get => users ??= new(this); }

    private SystemUserRepository? systemUsers;

    public SystemUserRepository SystemUsers { get => systemUsers ??= new(this); }

    private InstallationUserRepository? installationUsers;

    public InstallationUserRepository InstallationUsers { get => installationUsers ??= new(this); }

    internal AppUser User(AppUserEntity record) => new(this, record);

    private AppRepository? apps;

    public AppRepository Apps { get => apps ??= new(this); }

    internal App CreateApp(AppEntity record) => new(this, record);

    private AppVersionRepository? versions;

    public AppVersionRepository Versions { get => versions ??= new(this); }

    internal AppVersion CreateVersion(AppVersionEntity record) => new(this, record);

    private AppRoleRepository? roles;

    internal AppRoleRepository Roles { get => roles ??= new(this); }

    internal AppRole CreateRole(AppRoleEntity record) => new(this, record);

    private ResourceGroupRepository? groups;

    public ResourceGroupRepository Groups { get => groups ??= new(this); }

    internal ResourceGroup CreateGroup(ResourceGroupEntity record) => new(this, record);

    private ResourceRepository? resources;

    internal ResourceRepository Resources { get => resources ??= new(this); }

    internal Resource CreateResource(ResourceEntity record) => new(this, record);

    private ModifierCategoryRepository? modCategories;

    public ModifierCategoryRepository ModCategories { get => modCategories ??= new(this); }

    internal ModifierCategory ModCategory(ModifierCategoryEntity record) => new(this, record);

    private ModifierRepository? modifiers;

    public ModifierRepository Modifiers { get => modifiers ??= new(this); }

    internal Modifier CreateModifier(ModifierEntity record) => new(this, record);

    private AppSessionRepository? sessions;

    public AppSessionRepository Sessions { get => sessions ??= new(this); }

    internal AppSession CreateSession(AppSessionEntity record) => new(this, record);

    private AppRequestRepository? requests;

    public AppRequestRepository Requests { get => requests ??= new(this); }

    internal AppRequest CreateRequest(AppRequestEntity record) => new(this, record);

    private AppEventRepository? events;

    public AppEventRepository Events { get => events ??= new(this); }

    internal AppEvent CreateEvent(AppEventEntity record) => new(record);

    private InstallLocationRepository? installLocations;

    public InstallLocationRepository InstallLocations { get => installLocations ??= new(this); }

    internal InstallLocation CreateInstallLocation(InstallLocationEntity entity) => new(this, entity);

    private InstallationRepository? installations;

    public InstallationRepository Installations { get => installations ??= new(this); }

    internal Installation CreateInstallation(InstallationEntity entity)
        => entity.IsCurrent ? CurrentInstallation(entity) : VersionInstallation(entity);

    internal CurrentInstallation CurrentInstallation(InstallationEntity entity) => new(this, entity);

    internal VersionInstallation VersionInstallation(InstallationEntity entity) => new(this, entity);

    public Task Transaction(Func<Task> action) => DB.Transaction(action);
}