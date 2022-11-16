using XTI_Core;

namespace XTI_Hub.Abstractions;

public sealed class InstallationQueryRequest
{
    [NumericValue(typeof(InstallationQueryType))]
    public int QueryType { get; set; }
}
