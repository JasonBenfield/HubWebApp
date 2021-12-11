using System.Linq;
using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceGroupInquiry
{
    public sealed class GetResourcesRequest
    {
        public string VersionKey { get; set; }
        public int GroupID { get; set; }
    }
    public sealed class GetResourcesAction : AppAction<GetResourcesRequest, ResourceModel[]>
    {
        private readonly AppFromPath appFromPath;

        public GetResourcesAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceModel[]> Execute(GetResourcesRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var resourceGroup = await version.ResourceGroup(model.GroupID);
            var resources = await resourceGroup.Resources();
            return resources.Select(r => r.ToModel()).ToArray();
        }
    }
}
