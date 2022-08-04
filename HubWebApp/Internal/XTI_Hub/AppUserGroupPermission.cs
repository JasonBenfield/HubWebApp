namespace XTI_Hub;

public sealed record AppUserGroupPermission(AppUserGroup UserGroup, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}