using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Api;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.VersionInquiry
{
    public sealed class VersionInquiryGroup : AppApiGroupWrapper
    {
        public VersionInquiryGroup(AppApiGroup source, IServiceProvider services)
            : base(source)
        {
            var actions = new WebAppApiActionFactory(source);
            GetCurrentVersion = source.AddAction(actions.Action(nameof(GetCurrentVersion), () => CreateGetCurrentVersion(services)));
            GetVersion = source.AddAction(actions.Action(nameof(GetVersion), () => CreateGetVersion(services)));
            GetResourceGroup = source.AddAction(actions.Action(nameof(GetResourceGroup), () => CreateGetResourcegroup(services)));
        }

        internal GetCurrentVersionAction CreateGetCurrentVersion(IServiceProvider services)
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetCurrentVersionAction(appFromPath);
        }

        internal GetVersionAction CreateGetVersion(IServiceProvider services)
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetVersionAction(appFromPath);
        }

        internal GetResourceGroupAction CreateGetResourcegroup(IServiceProvider services)
        {
            var appFromPath = services.GetService<AppFromPath>();
            return new GetResourceGroupAction(appFromPath);
        }

        public AppApiAction<EmptyRequest, AppVersionModel> GetCurrentVersion { get; }
        public AppApiAction<string, AppVersionModel> GetVersion { get; }
        public AppApiAction<GetVersionResourceGroupRequest, ResourceGroupModel> GetResourceGroup { get; }
    }
}
