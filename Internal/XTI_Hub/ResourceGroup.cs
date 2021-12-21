﻿using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class ResourceGroup : IResourceGroup
{
    private readonly AppFactory factory;
    private readonly ResourceGroupEntity record;

    internal ResourceGroup(AppFactory factory, ResourceGroupEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ResourceGroupEntity();
        ID = new EntityID(this.record.ID);
    }

    public EntityID ID { get; }

    public ResourceGroupName Name() => new ResourceGroupName(record.Name);

    public Task<Resource> AddOrUpdateResource(ResourceName name, ResourceResultType resultType) =>
        factory.Resources.AddOrUpdate(this, name, resultType);

    async Task<IResource> IResourceGroup.Resource(ResourceName name) => await ResourceByName(name);

    public Task<Resource> ResourceByName(ResourceName name) =>
        factory.Resources.ResourceByName(this, name);

    public Task<Resource> ResourceOrDefault(ResourceName name) => 
        factory.Resources.ResourceOrDefault(this, name);

    public Task<Resource[]> Resources() => factory.Resources.Resources(this);

    public async Task<IEnumerable<Modifier>> Modifiers()
    {
        var modCategory = await factory.ModCategories.Category(record.ModCategoryID);
        var modifiers = await modCategory.Modifiers();
        return modifiers;
    }

    async Task<IModifierCategory> IResourceGroup.ModCategory() => await ModCategory();

    public Task<ModifierCategory> ModCategory() => 
        factory.ModCategories.Category(record.ModCategoryID);

    public Task AllowAnonymous() => setIsAnonymousAllowed(true);
    public Task DenyAnonymous() => setIsAnonymousAllowed(false);
    private Task setIsAnonymousAllowed(bool isAllowed)
        => factory.DB
            .ResourceGroups
            .Update
            (
                record,
                r =>
                {
                    r.IsAnonymousAllowed = isAllowed;
                }
            );

    public Task<AppRole[]> AllowedRoles()
        => factory.Roles.AllowedRolesForResourceGroup(this);

    public Task SetRoleAccess(IEnumerable<AppRole> allowedRoles)
        => factory.DB.Transaction(() => setRoleAccess(allowedRoles));

    private async Task setRoleAccess(IEnumerable<AppRole> allowedRoles)
    {
        await deleteExistingRoles(allowedRoles);
        var existingAllowedRoles = await AllowedRoles();
        foreach (var allowedRole in allowedRoles)
        {
            if (!existingAllowedRoles.Any(r => r.ID.Equals(allowedRole.ID.Value)))
            {
                await addGroupRole(allowedRole, true);
            }
        }
    }

    private async Task deleteExistingRoles(IEnumerable<AppRole> allowedRoles)
    {
        var allowedRoleIDs = allowedRoles.Select(r => r.ID.Value);
        var rolesToDelete = await factory.DB
            .ResourceGroupRoles
            .Retrieve()
            .Where
            (
                gr => gr.GroupID == ID.Value
                    &&
                    (
                        !allowedRoleIDs.Any(id => id == gr.RoleID) && gr.IsAllowed
                    )
            )
            .ToArrayAsync();
        foreach (var groupRole in rolesToDelete)
        {
            await factory.DB.ResourceGroupRoles.Delete(groupRole);
        }
    }

    private Task addGroupRole(AppRole role, bool isAllowed)
        => factory.DB
            .ResourceGroupRoles
            .Create
            (
                new ResourceGroupRoleEntity
                {
                    GroupID = ID.Value,
                    RoleID = role.ID.Value,
                    IsAllowed = isAllowed
                }
            );

    public Task<AppRequestExpandedModel[]> MostRecentRequests(int howMany)
        => factory.Requests.MostRecentForResourceGroup(this, howMany);

    public Task<AppEvent[]> MostRecentErrorEvents(int howMany)
        => factory.Events.MostRecentErrorsForResourceGroup(this, howMany);

    public ResourceGroupModel ToModel()
        => new ResourceGroupModel
        {
            ID = ID.Value,
            Name = Name().DisplayText,
            IsAnonymousAllowed = record.IsAnonymousAllowed,
            ModCategoryID = record.ModCategoryID
        };

    public override string ToString() => $"{nameof(ResourceGroup)} {ID.Value}";
}