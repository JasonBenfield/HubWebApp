using XTI_Hub.Abstractions;

namespace XTI_Admin;

public sealed class ProductionHubService
{
    public ProductionHubService(IHubService value)
    {
        Value = value;
    }

    public IHubService Value { get; }
}
