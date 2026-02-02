namespace XTI_Hub;

public sealed record AppUserGroupPermission(EfAppUserGroup EfUserGroup, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}