using XTI_Core;

namespace XTI_HubDB.Entities;

public interface IHubDbContext
{
    DataRepository<AppEntity> Apps { get; }
    DataRepository<LogEntryEntity> LogEntries { get; }
    DataRepository<ModifierCategoryEntity> ModifierCategories { get; }
    DataRepository<ModifierEntity> Modifiers { get; }
    DataRepository<AppRequestEntity> Requests { get; }
    DataRepository<ResourceGroupRoleEntity> ResourceGroupRoles { get; }
    DataRepository<ResourceGroupEntity> ResourceGroups { get; }
    DataRepository<ResourceRoleEntity> ResourceRoles { get; }
    DataRepository<ResourceEntity> Resources { get; }
    DataRepository<AppRoleEntity> Roles { get; }
    DataRepository<AppSessionEntity> Sessions { get; }
    DataRepository<UserGroupEntity> UserGroups { get; }
    DataRepository<AppUserEntity> Users { get; }
    DataRepository<AppUserRoleEntity> UserRoles { get; }
    DataRepository<AuthenticatorEntity> Authenticators { get; }
    DataRepository<UserAuthenticatorEntity> UserAuthenticators { get; }
    DataRepository<XtiVersionEntity> Versions { get; }
    DataRepository<AppXtiVersionEntity> AppVersions { get; }
    DataRepository<InstallLocationEntity> InstallLocations { get; }
    DataRepository<InstallationEntity> Installations { get; }
    DataRepository<StoredObjectEntity> StoredObjects { get; }
    DataRepository<ExpandedSession> ExpandedSessions { get; }
    DataRepository<ExpandedRequest> ExpandedRequests { get; }
    DataRepository<ExpandedLogEntry> ExpandedLogEntries { get; }
    Task Transaction(Func<Task> action);
    void ClearCache();
}