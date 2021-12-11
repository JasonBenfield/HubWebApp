using System.Linq;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_App.Api;
using XTI_HubAppClient;

namespace XTI_AppSetupApp.Extensions
{
    public sealed class DefaultAppSetup : IAppSetup
    {
        private readonly HubAppClient hubClient;
        private readonly AppApiFactory apiFactory;
        private readonly VersionReader versionReader;

        public DefaultAppSetup(HubAppClient hubApi, AppApiFactory apiFactory, VersionReader versionReader)
        {
            this.hubClient = hubApi;
            this.apiFactory = apiFactory;
            this.versionReader = versionReader;
        }

        public async Task Run(AppVersionKey versionKey)
        {
            var versions = await versionReader.Versions();
            var template = apiFactory.CreateTemplate();
            var request = new RegisterAppRequest
            {
                AppTemplate = ToClientTemplateModel(template.ToModel()),
                VersionKey = versionKey,
                Versions = versions
            };
            await hubClient.Install.RegisterApp("", request);
        }

        private XTI_HubAppClient.AppApiTemplateModel ToClientTemplateModel(XTI_App.Api.AppApiTemplateModel model)
        {
            return new XTI_HubAppClient.AppApiTemplateModel
            {
                AppKey = new XTI_HubAppClient.AppKey
                {
                    Name = model.AppKey.Name,
                    Type = XTI_HubAppClient.AppType.Values.Value(model.AppKey.Type.Value)
                },
                GroupTemplates = model.GroupTemplates
                    .Select
                    (
                        gt => new XTI_HubAppClient.AppApiGroupTemplateModel
                        {
                            Name = gt.Name,
                            IsAnonymousAllowed = gt.IsAnonymousAllowed,
                            ModCategory = gt.ModCategory,
                            Roles = gt.Roles,
                            ActionTemplates = gt.ActionTemplates
                                .Select
                                (
                                    at => new XTI_HubAppClient.AppApiActionTemplateModel
                                    {
                                        Name = at.Name,
                                        IsAnonymousAllowed = at.IsAnonymousAllowed,
                                        Roles = at.Roles,
                                        ResultType = XTI_HubAppClient.ResourceResultType.Values.Value(at.ResultType.Value)
                                    }
                                )
                                .ToArray()
                        }
                    )
                    .ToArray()
            };
        }
    }
}
