using XTI_App;
using XTI_App.Api;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.VersionInquiry
{
    public sealed class VersionInquiryGroup : AppApiGroupWrapper
    {
        public VersionInquiryGroup(AppApiGroup source, VersionInquiryActionFactory factory)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            GetVersion = source.AddAction(actions.Action(nameof(GetVersion), factory.CreateGetVersion));
            GetCurrentVersion = source.AddAction(actions.Action(nameof(GetCurrentVersion), factory.CreateGetCurrentVersion));
            GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), factory.CreateGetResourcegroup));
        }

        public AppApiAction<string, AppVersionModel> GetVersion { get; }
        public AppApiAction<EmptyRequest, AppVersionModel> GetCurrentVersion { get; }
        public AppApiAction<GetVersionResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    }
}
