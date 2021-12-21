using Microsoft.EntityFrameworkCore;
using XTI_Core;
using XTI_Core.EF;
using XTI_HubDB.Entities;

namespace XTI_HubDB.EF;

public sealed class HubDbContext : DbContext, IHubDbContext
{
    private readonly UnitOfWork unitOfWork;

    public HubDbContext(DbContextOptions<HubDbContext> options)
        : base(options)
    {
        Users = new EfDataRepository<AppUserEntity>(this);
        Sessions = new EfDataRepository<AppSessionEntity>(this);
        Requests = new EfDataRepository<AppRequestEntity>(this);
        Events = new EfDataRepository<AppEventEntity>(this);
        Apps = new EfDataRepository<AppEntity>(this);
        Versions = new EfDataRepository<AppVersionEntity>(this);
        Roles = new EfDataRepository<AppRoleEntity>(this);
        UserRoles = new EfDataRepository<AppUserRoleEntity>(this);
        ResourceGroups = new EfDataRepository<ResourceGroupEntity>(this);
        ResourceGroupRoles = new EfDataRepository<ResourceGroupRoleEntity>(this);
        Resources = new EfDataRepository<ResourceEntity>(this);
        ResourceRoles = new EfDataRepository<ResourceRoleEntity>(this);
        ModifierCategories = new EfDataRepository<ModifierCategoryEntity>(this);
        Modifiers = new EfDataRepository<ModifierEntity>(this);
        InstallLocations = new EfDataRepository<InstallLocationEntity>(this);
        Installations = new EfDataRepository<InstallationEntity>(this);
        unitOfWork = new UnitOfWork(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AppUserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppSessionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppRequestEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppEventEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppVersionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceGroupEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceGroupRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceRoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModifierCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ModifierEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallLocationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new InstallationEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DataRepository<AppUserEntity> Users { get; }
    public DataRepository<AppSessionEntity> Sessions { get; }
    public DataRepository<AppRequestEntity> Requests { get; }
    public DataRepository<AppEventEntity> Events { get; }
    public DataRepository<AppEntity> Apps { get; }
    public DataRepository<AppVersionEntity> Versions { get; }
    public DataRepository<AppRoleEntity> Roles { get; }
    public DataRepository<AppUserRoleEntity> UserRoles { get; }
    public DataRepository<ResourceGroupEntity> ResourceGroups { get; }
    public DataRepository<ResourceGroupRoleEntity> ResourceGroupRoles { get; }
    public DataRepository<ResourceEntity> Resources { get; }
    public DataRepository<ResourceRoleEntity> ResourceRoles { get; }
    public DataRepository<ModifierCategoryEntity> ModifierCategories { get; }
    public DataRepository<ModifierEntity> Modifiers { get; }
    public DataRepository<InstallLocationEntity> InstallLocations { get; }
    public DataRepository<InstallationEntity> Installations { get; }

    public Task Transaction(Func<Task> action) => unitOfWork.Execute(action);

}