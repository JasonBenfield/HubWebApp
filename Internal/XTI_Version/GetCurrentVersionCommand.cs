using System;
using System.Threading.Tasks;
using XTI_App.Abstractions;
using XTI_HubAppApi;
using XTI_HubAppApi.AppRegistration;
using XTI_VersionToolApi;

namespace XTI_Version
{
    public sealed class GetCurrentVersionCommand : VersionCommand
    {
        private readonly HubAppApi hubApi;

        public GetCurrentVersionCommand(HubAppApi hubApi)
        {
            this.hubApi = hubApi;
        }

        public async Task Execute(VersionToolOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AppName)) { throw new ArgumentException("App Name is required"); }
            if (string.IsNullOrWhiteSpace(options.AppType)) { throw new ArgumentException("App Type is required"); }
            var currentVersion = await hubApi.AppRegistration.GetVersion.Invoke(new GetVersionRequest
            {
                AppKey = options.AppKey(),
                VersionKey = AppVersionKey.Current
            });
            var output = new VersionOutput();
            output.Output(currentVersion, options.OutputPath);
        }
    }
}
