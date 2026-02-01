namespace XTI_Hub;

public sealed record AppPermission(EfApp App, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}
