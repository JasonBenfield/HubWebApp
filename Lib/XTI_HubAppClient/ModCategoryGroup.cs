// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ModCategoryGroup : AppClientGroup
{
    public ModCategoryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl) : base(httpClientFactory, xtiTokenAccessor, clientUrl, "ModCategory")
    {
    }

    public Task<ModifierCategoryModel> GetModCategory(string modifier, int model) => Post<ModifierCategoryModel, int>("GetModCategory", modifier, model);
    public Task<ModifierModel[]> GetModifiers(string modifier, int model) => Post<ModifierModel[], int>("GetModifiers", modifier, model);
    public Task<ModifierModel> GetModifier(string modifier, GetModCategoryModifierRequest model) => Post<ModifierModel, GetModCategoryModifierRequest>("GetModifier", modifier, model);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, int model) => Post<ResourceGroupModel[], int>("GetResourceGroups", modifier, model);
}