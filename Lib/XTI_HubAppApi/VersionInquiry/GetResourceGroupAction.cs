using System.Threading.Tasks;
using XTI_App;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.VersionInquiry
{
    public sealed class GetVersionResourceGroupRequest
    {
        public string VersionKey { get; set; }
        public string GroupName { get; set; }
    }
    public sealed class GetResourceGroupAction : AppAction<GetVersionResourceGroupRequest, ResourceGroupModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceGroupAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceGroupModel> Execute(GetVersionResourceGroupRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var groupName = new ResourceGroupName(model.GroupName);
            var group = await version.ResourceGroup(groupName);
            var groupModel = group.ToModel();
            return groupModel;
        }
    }
}
