using XTI_Core;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class EfHubDB
{
    public EfHubDB(HubDbContext db)
    {
        Context = db;
    }

    internal HubDbContext Context { get; }

    private EfAppUserGroups? userGroups;

    public EfAppUserGroups UserGroups { get => userGroups ??= new(this); }

    private EfAppUsers? users;

    public EfAppUsers Users { get => users ??= new(this); }

    private EfAppUserRoles? userRoles;

    public EfAppUserRoles UserRoles { get => userRoles ??= new(this); }

    private EfSystemUsers? systemUsers;

    public EfSystemUsers SystemUsers { get => systemUsers ??= new(this); }

    private EfInstallationUsers? installers;

    public EfInstallationUsers Installers { get => installers ??= new(this); }

    private EfApps? apps;

    public EfApps Apps { get => apps ??= new(this); }

    private EfVersions? versions;

    public EfVersions Versions { get => versions ??= new(this); }

    private EfAppRoles? roles;

    internal EfAppRoles Roles { get => roles ??= new(this); }

    private EfResourceGroups? groups;

    public EfResourceGroups Groups { get => groups ??= new(this); }

    private EfResources? resources;

    internal EfResources Resources { get => resources ??= new(this); }

    private EfModifierCategories? modCategories;

    public EfModifierCategories ModCategories { get => modCategories ??= new(this); }

    internal EfModifierCategory ModCategory(ModifierCategoryEntity record) => new(this, record);

    private EfModifiers? modifiers;

    public EfModifiers Modifiers { get => modifiers ??= new(this); }

    private EfAppSessions? sessions;

    public EfAppSessions Sessions { get => sessions ??= new(this); }

    private EfAppRequests? requests;

    public EfAppRequests Requests { get => requests ??= new(this); }

    private EfAuthenticators? authenticators;

    public EfAuthenticators Authenticators { get => authenticators ??= new(this); }

    private EfLogEntries? logEntries;

    public EfLogEntries LogEntries { get => logEntries ??= new(this); }

    private EfInstallConfigurations? installConfigurations;

    public EfInstallConfigurations InstallConfigurations { get => installConfigurations ??= new(this); }

    private EfInstallConfigurationTemplates? installConfigurationTemplates;

    public EfInstallConfigurationTemplates InstallConfigurationTemplates { get => installConfigurationTemplates ??= new(this); }

    private EfInstallLocations? installLocations;

    public EfInstallLocations InstallLocations { get => installLocations ??= new(this); }

    private EfAppCommands? appCommands;

    public EfAppCommands AppCommands { get => appCommands ??= new(this); }

    private EfAppCommandSteps? appCommandSteps;

    public EfAppCommandSteps AppCommandSteps { get => appCommandSteps ??= new(this); }

    private EfInstallations? installations;

    public EfInstallations Installations { get => installations ??= new(this); }

    private EfStoredObjects? storedObjects;

    public EfStoredObjects StoredObjects { get => storedObjects ??= new(this); }

    public DataRepository<ExpandedUser> ExpandedUsers { get => Context.ExpandedUsers; }

    public DataRepository<ExpandedUserRole> ExpandedUserRoles { get => Context.ExpandedUserRoles; }

    public DataRepository<ExpandedSession> ExpandedSessions { get => Context.ExpandedSessions; }

    public DataRepository<ExpandedRequest> ExpandedRequests { get => Context.ExpandedRequests; }

    public DataRepository<ExpandedLogEntry> ExpandedLogEntries { get => Context.ExpandedLogEntries; }

    public DataRepository<ExpandedInstallation> ExpandedInstallations { get => Context.ExpandedInstallations; }

    public void SetTimeout(TimeSpan timeout) => Context.SetTimeout(timeout);

    public Task Transaction(Func<Task> action) => Context.Transaction(action);

    public Task<T> Transaction<T>(Func<Task<T>> action) => Context.Transaction(action);
}