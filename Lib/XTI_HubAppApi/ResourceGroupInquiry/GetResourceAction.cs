using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceGroupInquiry
{
    public sealed class GetResourceGroupResourceRequest
    {
        public string VersionKey { get; set; }
        public int GroupID { get; set; }
        public string ResourceName { get; set; }
    }
    public sealed class GetResourceAction : AppAction<GetResourceGroupResourceRequest, ResourceModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceModel> Execute(GetResourceGroupResourceRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var group = await version.ResourceGroup(model.GroupID);
            var resourceName = new ResourceName(model.ResourceName);
            var resource = await group.Resource(resourceName);
            var resourceModel = resource.ToModel();
            return resourceModel;
        }
    }
}
