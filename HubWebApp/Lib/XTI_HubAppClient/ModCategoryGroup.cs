// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ModCategoryGroup : AppClientGroup
{
    public ModCategoryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ModCategory")
    {
        Actions = new ModCategoryGroupActions(GetModCategory: CreatePostAction<int, ModifierCategoryModel>("GetModCategory"), GetModifiers: CreatePostAction<int, ModifierModel[]>("GetModifiers"), GetResourceGroups: CreatePostAction<int, ResourceGroupModel[]>("GetResourceGroups"));
    }

    public ModCategoryGroupActions Actions { get; }

    public Task<ModifierCategoryModel> GetModCategory(string modifier, int model) => Actions.GetModCategory.Post(modifier, model);
    public Task<ModifierModel[]> GetModifiers(string modifier, int model) => Actions.GetModifiers.Post(modifier, model);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, int model) => Actions.GetResourceGroups.Post(modifier, model);
    public sealed record ModCategoryGroupActions(AppClientPostAction<int, ModifierCategoryModel> GetModCategory, AppClientPostAction<int, ModifierModel[]> GetModifiers, AppClientPostAction<int, ResourceGroupModel[]> GetResourceGroups);
}