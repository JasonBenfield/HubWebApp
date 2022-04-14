using Microsoft.Extensions.Options;
using XTI_WebApp.Abstractions;

namespace XTI_HubAppClient.Extensions;

internal sealed class HubClientOptionsAppClientDomain : IAppClientDomain
{
    private readonly HubClientOptions options;

    public HubClientOptionsAppClientDomain(HubClientOptions options)
    {
        this.options = options;
    }

    public Task<string> Value(string appName, string version)
    {
        string domain;
        if (appName.Equals("Hub", StringComparison.OrdinalIgnoreCase))
        {
            domain = options.Domain;
        }
        else
        {
            domain = "";
        }
        return Task.FromResult(domain);
    }
}