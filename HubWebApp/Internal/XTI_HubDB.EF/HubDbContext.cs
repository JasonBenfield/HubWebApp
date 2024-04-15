using XTI_Core;
using XTI_Core.EF;

namespace XTI_HubDB.EF;

public sealed class HubDbContext : DbContext, IHubDbContext
{
    private readonly UnitOfWork unitOfWork;

    public HubDbContext(DbContextOptions<HubDbContext> options)
        : base(options)
    {
        Authenticators = new EfDataRepository<AuthenticatorEntity>(this);
        UserGroups = new EfDataRepository<UserGroupEntity>(this);
        Users = new EfDataRepository<AppUserEntity>(this);
        UserAuthenticators = new EfDataRepository<UserAuthenticatorEntity>(this);
        Sessions = new EfDataRepository<AppSessionEntity>(this);
        Requests = new EfDataRepository<AppRequestEntity>(this);
        SourceRequests = new EfDataRepository<SourceRequestEntity>(this);
        LogEntries = new EfDataRepository<LogEntryEntity>(this);
        SourceLogEntries = new EfDataRepository<SourceLogEntryEntity>(this);
        Apps = new EfDataRepository<AppEntity>(this);
        Versions = new EfDataRepository<XtiVersionEntity>(this);
        AppVersions = new EfDataRepository<AppXtiVersionEntity>(this);
        Roles = new EfDataRepository<AppRoleEntity>(this);
        UserRoles = new EfDataRepository<AppUserRoleEntity>(this);
        ResourceGroups = new EfDataRepository<ResourceGroupEntity>(this);
        ResourceGroupRoles = new EfDataRepository<ResourceGroupRoleEntity>(this);
        Resources = new EfDataRepository<ResourceEntity>(this);
        ResourceRoles = new EfDataRepository<ResourceRoleEntity>(this);
        ModifierCategories = new EfDataRepository<ModifierCategoryEntity>(this);
        Modifiers = new EfDataRepository<ModifierEntity>(this);
        InstallConfigurations = new EfDataRepository<InstallConfigurationEntity>(this);
        InstallConfigurationTemplates = new EfDataRepository<InstallConfigurationTemplateEntity>(this);
        InstallLocations = new EfDataRepository<InstallLocationEntity>(this);
        Installations = new EfDataRepository<InstallationEntity>(this);
        StoredObjects = new EfDataRepository<StoredObjectEntity>(this);
        ExpandedSessions = new EfDataRepository<ExpandedSession>(this);
        ExpandedRequests = new EfDataRepository<ExpandedRequest>(this);
        ExpandedLogEntries = new EfDataRepository<ExpandedLogEntry>(this);
        ExpandedInstallations = new EfDataRepository<ExpandedInstallation>(this);
        ExpandedUserRoles = new EfDataRepository<ExpandedUserRole>(this);
        unitOfWork = new UnitOfWork(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserAuthenticatorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AuthenticatorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppSessionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppRequestEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SourceRequestEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LogEntryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SourceLogEntryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppEntityConfiguration());
        modelBuilder.ApplyConfiguration(new XtiVersionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppXtiVersionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceGroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceGroupRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModifierCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModifierEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallConfigurationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallConfigurationTemplateEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallLocationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StoredObjectEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpandedSessionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpandedRequestEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpandedLogEntryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpandedInstallationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExpandedUserRoleEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DataRepository<AuthenticatorEntity> Authenticators { get; }
    public DataRepository<UserGroupEntity> UserGroups { get; }
    public DataRepository<AppUserEntity> Users { get; }
    public DataRepository<UserAuthenticatorEntity> UserAuthenticators { get; }
    public DataRepository<AppSessionEntity> Sessions { get; }
    public DataRepository<AppRequestEntity> Requests { get; }
    public DataRepository<SourceRequestEntity> SourceRequests { get; }
    public DataRepository<LogEntryEntity> LogEntries { get; }
    public DataRepository<SourceLogEntryEntity> SourceLogEntries { get; }
    public DataRepository<AppEntity> Apps { get; }
    public DataRepository<XtiVersionEntity> Versions { get; }
    public DataRepository<AppXtiVersionEntity> AppVersions { get; }
    public DataRepository<AppRoleEntity> Roles { get; }
    public DataRepository<AppUserRoleEntity> UserRoles { get; }
    public DataRepository<ResourceGroupEntity> ResourceGroups { get; }
    public DataRepository<ResourceGroupRoleEntity> ResourceGroupRoles { get; }
    public DataRepository<ResourceEntity> Resources { get; }
    public DataRepository<ResourceRoleEntity> ResourceRoles { get; }
    public DataRepository<ModifierCategoryEntity> ModifierCategories { get; }
    public DataRepository<ModifierEntity> Modifiers { get; }
    public DataRepository<InstallConfigurationEntity> InstallConfigurations { get; }
    public DataRepository<InstallConfigurationTemplateEntity> InstallConfigurationTemplates { get; }
    public DataRepository<InstallLocationEntity> InstallLocations { get; }
    public DataRepository<InstallationEntity> Installations { get; }
    public DataRepository<StoredObjectEntity> StoredObjects { get; }
    public DataRepository<ExpandedSession> ExpandedSessions { get; }
    public DataRepository<ExpandedRequest> ExpandedRequests { get; }
    public DataRepository<ExpandedLogEntry> ExpandedLogEntries { get; }
    public DataRepository<ExpandedInstallation> ExpandedInstallations { get; }
    public DataRepository<ExpandedUserRole> ExpandedUserRoles { get; }

    public Task Transaction(Func<Task> action) => unitOfWork.Execute(action);

    public Task<TResult> Transaction<TResult>(Func<Task<TResult>> action) => unitOfWork.Execute(action);

    public void SetTimeout(TimeSpan timeout) => Database.SetCommandTimeout(timeout);

    public void ClearCache() => ChangeTracker.Clear();
}