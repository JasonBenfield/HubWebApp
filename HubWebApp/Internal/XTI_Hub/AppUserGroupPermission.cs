namespace XTI_Hub;

public sealed record AppUserGroupPermission(EfAppUserGroup UserGroup, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}