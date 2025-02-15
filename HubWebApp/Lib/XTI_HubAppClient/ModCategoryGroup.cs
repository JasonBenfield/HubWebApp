// Generated Code
namespace XTI_HubAppClient;
public sealed partial class ModCategoryGroup : AppClientGroup
{
    public ModCategoryGroup(IHttpClientFactory httpClientFactory, XtiTokenAccessor xtiTokenAccessor, AppClientUrl clientUrl, AppClientOptions options) : base(httpClientFactory, xtiTokenAccessor, clientUrl, options, "ModCategory")
    {
        Actions = new ModCategoryGroupActions(GetModCategory: CreatePostAction<int, ModifierCategoryModel>("GetModCategory"), GetModifiers: CreatePostAction<int, ModifierModel[]>("GetModifiers"), GetResourceGroups: CreatePostAction<int, ResourceGroupModel[]>("GetResourceGroups"));
        Configure();
    }

    partial void Configure();
    public ModCategoryGroupActions Actions { get; }

    public Task<ModifierCategoryModel> GetModCategory(string modifier, int requestData, CancellationToken ct = default) => Actions.GetModCategory.Post(modifier, requestData, ct);
    public Task<ModifierModel[]> GetModifiers(string modifier, int requestData, CancellationToken ct = default) => Actions.GetModifiers.Post(modifier, requestData, ct);
    public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, int requestData, CancellationToken ct = default) => Actions.GetResourceGroups.Post(modifier, requestData, ct);
    public sealed record ModCategoryGroupActions(AppClientPostAction<int, ModifierCategoryModel> GetModCategory, AppClientPostAction<int, ModifierModel[]> GetModifiers, AppClientPostAction<int, ResourceGroupModel[]> GetResourceGroups);
}