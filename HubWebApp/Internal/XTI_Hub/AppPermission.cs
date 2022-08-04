namespace XTI_Hub;

public sealed record AppPermission(App App, bool CanView, bool CanEdit)
{
    public bool HasAccess() => CanView || CanEdit;
}
