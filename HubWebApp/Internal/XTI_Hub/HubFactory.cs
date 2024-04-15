using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class HubFactory
{
    public HubFactory(IHubDbContext db)
    {
        DB = db;
    }

    internal IHubDbContext DB { get; }

    private AppUserGroupRepository? userGroups;

    public AppUserGroupRepository UserGroups { get => userGroups ??= new(this); }

    private AppUserRepository? users;

    public AppUserRepository Users { get => users ??= new(this); }

    private AppUserRoleRepository? userRoles;

    public AppUserRoleRepository UserRoles { get => userRoles ??= new(this); }

    private SystemUserRepository? systemUsers;

    public SystemUserRepository SystemUsers { get => systemUsers ??= new(this); }

    private InstallerRepository? installers;

    public InstallerRepository Installers { get => installers ??= new(this); }

    internal AppUser User(AppUserEntity record) => new(this, record);

    private AppRepository? apps;

    public AppRepository Apps { get => apps ??= new(this); }

    internal App CreateApp(AppEntity record) => new(this, record);

    private XtiVersionRepository? versions;

    public XtiVersionRepository Versions { get => versions ??= new(this); }

    internal XtiVersion CreateVersion(XtiVersionEntity record) => new(this, record);

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


    private AuthenticatorRepository? authenticators;

    public AuthenticatorRepository Authenticators { get => authenticators ??= new(this); }

    private LogEntryRepository? logEntries;

    public LogEntryRepository LogEntries { get => logEntries ??= new(this); }

    internal LogEntry CreateLogEntry(LogEntryEntity record) => new(this, record);

    private InstallConfigurationRepository? installConfigurations;

    public InstallConfigurationRepository InstallConfigurations { get => installConfigurations ??= new(this); }

    private InstallConfigurationTemplateRepository? installConfigurationTemplates;

    internal InstallConfigurationTemplateRepository InstallConfigurationTemplates { get => installConfigurationTemplates ??= new(this); }

    private InstallLocationRepository? installLocations;

    public InstallLocationRepository InstallLocations { get => installLocations ??= new(this); }

    internal InstallLocation CreateInstallLocation(InstallLocationEntity entity) => new(this, entity);

    private InstallationRepository? installations;

    public InstallationRepository Installations { get => installations ??= new(this); }

    private StoredObjectRepository? storedObjects;

    public StoredObjectRepository StoredObjects { get => storedObjects ??= new(this); }

    internal Installation CreateInstallation(InstallationEntity entity)
        => new Installation(this, entity);

    public Task Transaction(Func<Task> action) => DB.Transaction(action);
}