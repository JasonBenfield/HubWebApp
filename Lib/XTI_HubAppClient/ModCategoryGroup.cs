// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class ModCategoryGroup : AppClientGroup
    {
        public ModCategoryGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl) : base(httpClientFactory, xtiToken, baseUrl, "ModCategory")
        {
        }

        public Task<ModifierCategoryModel> GetModCategory(string modifier, int model) => Post<ModifierCategoryModel, int>("GetModCategory", modifier, model);
        public Task<ModifierModel[]> GetModifiers(string modifier, int model) => Post<ModifierModel[], int>("GetModifiers", modifier, model);
        public Task<ResourceGroupModel[]> GetResourceGroups(string modifier, int model) => Post<ResourceGroupModel[], int>("GetResourceGroups", modifier, model);
    }
}