namespace XTI_Hub;

public sealed record AppPermission(EfApp EfApp, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}
