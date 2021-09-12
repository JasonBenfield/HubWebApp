using Microsoft.Extensions.DependencyInjection;
using System;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_Core;
using XTI_Hub;
using XTI_WebApp.Api;

namespace XTI_HubAppApi.AppRegistration
{
    public sealed class AppRegistrationGroup : AppApiGroupWrapper
    {
        private readonly IServiceProvider services;

        public AppRegistrationGroup(AppApiGroup source, IServiceProvider services)
            : base(source)
        {
            this.services = services;
            var actions = new WebAppApiActionFactory(source);
            RegisterApp = source.AddAction(actions.Action(nameof(RegisterApp), createRegisterApp));
            NewVersion = source.AddAction(actions.Action(nameof(NewVersion), createNewVersion));
            BeginPublish = source.AddAction(actions.Action(nameof(BeginPublish), createBeginPublish));
            EndPublish = source.AddAction(actions.Action(nameof(EndPublish), createEndPublish));
            GetVersions = source.AddAction(actions.Action(nameof(GetVersions), createGetVersions));
            GetVersion = source.AddAction(actions.Action(nameof(GetVersion), createGetVersion));
            AddSystemUser = source.AddAction(actions.Action(nameof(AddSystemUser), createAddSystemUser));
        }

        private RegisterAppAction createRegisterApp()
        {
            var appFactory = services.GetService<AppFactory>();
            var clock = services.GetService<Clock>();
            return new RegisterAppAction(appFactory, clock);
        }

        private NewVersionAction createNewVersion()
        {
            var appFactory = services.GetService<AppFactory>();
            var clock = services.GetService<Clock>();
            return new NewVersionAction(appFactory, clock);
        }

        private BeginPublishAction createBeginPublish()
        {
            var appFactory = services.GetService<AppFactory>();
            return new BeginPublishAction(appFactory);
        }

        private EndPublishAction createEndPublish()
        {
            var appFactory = services.GetService<AppFactory>();
            return new EndPublishAction(appFactory);
        }

        private GetVersionsAction createGetVersions()
        {
            var appFactory = services.GetService<AppFactory>();
            return new GetVersionsAction(appFactory);
        }

        private GetVersionAction createGetVersion()
        {
            var appFactory = services.GetService<AppFactory>();
            return new GetVersionAction(appFactory);
        }

        private AddSystemUserAction createAddSystemUser()
        {
            var appFactory = services.GetService<AppFactory>();
            var clock = services.GetService<Clock>();
            var hashedPasswordFactory = services.GetService<IHashedPasswordFactory>();
            return new AddSystemUserAction(appFactory, clock, hashedPasswordFactory);
        }

        public AppApiAction<RegisterAppRequest, EmptyActionResult> RegisterApp { get; }
        public AppApiAction<NewVersionRequest, AppVersionModel> NewVersion { get; }
        public AppApiAction<GetVersionRequest, AppVersionModel> BeginPublish { get; }
        public AppApiAction<GetVersionRequest, AppVersionModel> EndPublish { get; }
        public AppApiAction<AppKey, AppVersionModel[]> GetVersions { get; }
        public AppApiAction<GetVersionRequest, AppVersionModel> GetVersion { get; }
        public AppApiAction<AddSystemUserRequest, AppUserModel> AddSystemUser { get; }
    }
}
