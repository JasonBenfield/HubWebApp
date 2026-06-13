namespace XTI_HubWebAppApiActions;

internal sealed class AppKeyFromPath
{
    private readonly IModifierKeyAccessor modifierKeyAccessor;

    internal AppKeyFromPath(IModifierKeyAccessor modifierKeyAccessor)
    {
        this.modifierKeyAccessor = modifierKeyAccessor;
    }

    internal AppKey Value()
    {
        var modKey = modifierKeyAccessor.Value();
        if (modKey.Equals(ModifierKey.Default))
        {
            throw new Exception(AppErrors.ModifierIsRequired);
        }
        return AppKey.Parse(modKey.DisplayText);
    }
}