using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class ProductionHubAdmin
{
    public ProductionHubAdmin(IHubAdministration value)
    {
        Value = value;
    }

    public IHubAdministration Value { get; }
}
