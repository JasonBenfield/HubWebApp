// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class PublishGroup : AppClientGroup
    {
        public PublishGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "Publish")
        {
        }

        public Task<AppVersionModel> NewVersion(string modifier, NewVersionRequest model) => Post<AppVersionModel, NewVersionRequest>("NewVersion", modifier, model);
        public Task<AppVersionModel> BeginPublish(string modifier, PublishVersionRequest model) => Post<AppVersionModel, PublishVersionRequest>("BeginPublish", modifier, model);
        public Task<AppVersionModel> EndPublish(string modifier, PublishVersionRequest model) => Post<AppVersionModel, PublishVersionRequest>("EndPublish", modifier, model);
        public Task<AppVersionModel[]> GetVersions(string modifier, AppKey model) => Post<AppVersionModel[], AppKey>("GetVersions", modifier, model);
    }
}