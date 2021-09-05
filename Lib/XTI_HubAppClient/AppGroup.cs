// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class AppGroup : AppClientGroup
    {
        public AppGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl) : base(httpClientFactory, xtiToken, baseUrl, "App")
        {
        }

        public Task<AppModel> GetApp(string modifier) => Post<AppModel, EmptyRequest>("GetApp", modifier, new EmptyRequest());
        public Task<AppVersionModel> GetCurrentVersion(string modifier) => Post<AppVersionModel, EmptyRequest>("GetCurrentVersion", modifier, new EmptyRequest());
        public Task<ResourceGroupModel[]> GetResourceGroups(string modifier) => Post<ResourceGroupModel[], EmptyRequest>("GetResourceGroups", modifier, new EmptyRequest());
        public Task<AppRequestExpandedModel[]> GetMostRecentRequests(string modifier, int model) => Post<AppRequestExpandedModel[], int>("GetMostRecentRequests", modifier, model);
        public Task<AppEventModel[]> GetMostRecentErrorEvents(string modifier, int model) => Post<AppEventModel[], int>("GetMostRecentErrorEvents", modifier, model);
        public Task<ModifierCategoryModel[]> GetModifierCategories(string modifier) => Post<ModifierCategoryModel[], EmptyRequest>("GetModifierCategories", modifier, new EmptyRequest());
    }
}