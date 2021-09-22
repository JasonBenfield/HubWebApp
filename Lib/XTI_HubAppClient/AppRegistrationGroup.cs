// Generated Code
using XTI_WebAppClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XTI_HubAppClient
{
    public sealed partial class AppRegistrationGroup : AppClientGroup
    {
        public AppRegistrationGroup(IHttpClientFactory httpClientFactory, IXtiToken xtiToken, string baseUrl): base(httpClientFactory, xtiToken, baseUrl, "AppRegistration")
        {
        }

        public Task<EmptyActionResult> RegisterApp(string modifier, RegisterAppRequest model) => Post<EmptyActionResult, RegisterAppRequest>("RegisterApp", modifier, model);
        public Task<AppVersionModel> NewVersion(string modifier, NewVersionRequest model) => Post<AppVersionModel, NewVersionRequest>("NewVersion", modifier, model);
        public Task<AppVersionModel> BeginPublish(string modifier, GetVersionRequest model) => Post<AppVersionModel, GetVersionRequest>("BeginPublish", modifier, model);
        public Task<AppVersionModel> EndPublish(string modifier, GetVersionRequest model) => Post<AppVersionModel, GetVersionRequest>("EndPublish", modifier, model);
        public Task<AppVersionModel[]> GetVersions(string modifier, AppKey model) => Post<AppVersionModel[], AppKey>("GetVersions", modifier, model);
        public Task<AppVersionModel> GetVersion(string modifier, GetVersionRequest model) => Post<AppVersionModel, GetVersionRequest>("GetVersion", modifier, model);
        public Task<AppUserModel> AddSystemUser(string modifier, AddSystemUserRequest model) => Post<AppUserModel, AddSystemUserRequest>("AddSystemUser", modifier, model);
    }
}