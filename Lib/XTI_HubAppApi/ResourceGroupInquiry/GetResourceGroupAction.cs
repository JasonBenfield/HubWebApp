using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceGroupInquiry
{
    public sealed class GetResourceGroupRequest
    {
        public string VersionKey { get; set; }
        public int GroupID { get; set; }
    }
    public sealed class GetResourceGroupAction : AppAction<GetResourceGroupRequest, ResourceGroupModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceGroupModel> Execute(GetResourceGroupRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var group = await version.ResourceGroup(model.GroupID);
            return group.ToModel();
        }
    }
}
