using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_App.Api;

namespace XTI_HubAppApi.AppList;

public sealed class GetAppDomainRequest
{
    public string AppName { get; set; } = "";
    public string Version { get; set; } = "";
}

public sealed class GetAppDomainAction : AppAction<GetAppDomainRequest, string>
{
    public Task<string> Execute(GetAppDomainRequest model)
    {
        throw new NotImplementedException();
    }
}