using System.Threading.Tasks;
using XTI_Hub;
using XTI_App.Abstractions;
using XTI_App.Api;

namespace XTI_HubAppApi.ResourceInquiry
{
    public sealed class GetResourceRequest
    {
        public string VersionKey { get; set; }
        public int ResourceID { get; set; }
    }

    public sealed class GetResourceAction : AppAction<GetResourceRequest, ResourceModel>
    {
        private readonly AppFromPath appFromPath;

        public GetResourceAction(AppFromPath appFromPath)
        {
            this.appFromPath = appFromPath;
        }

        public async Task<ResourceModel> Execute(GetResourceRequest model)
        {
            var app = await appFromPath.Value();
            var versionKey = AppVersionKey.Parse(model.VersionKey);
            var version = await app.Version(versionKey);
            var resource = await version.Resource(model.ResourceID);
            return resource.ToModel();
        }
    }
}
