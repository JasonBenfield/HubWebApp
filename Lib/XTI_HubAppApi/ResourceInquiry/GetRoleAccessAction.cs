using System.Linq;
using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class GetResourceRoleAccessRequest
    {
        public string VersionKey { get; set; }
        public int ResourceID { get; set; }
    }
    public sealed class GetRoleAccessAction : AppAction<GetResourceRoleAccessRequest, AppRoleModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetRoleAccessAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<AppRoleModel[]> Execute(GetResourceRoleAccessRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var resource = await version.Resource(model.ResourceID);
            var allowedRoles = await resource.AllowedRoles();
            var allowedRoleModels = allowedRoles.Select(ar => ar.ToModel()).ToArray();
            return allowedRoleModels;
        }
    }
}
