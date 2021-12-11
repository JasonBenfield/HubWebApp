using XTI_App.Abstractions;

namespace XTI_HubAppApi.AppInstall
{
    public sealed class AddSystemUserRequest
    {
        public AppKey AppKey { get; set; }
        public string MachineName { get; set; }
        public string Password { get; set; }
    }
}
