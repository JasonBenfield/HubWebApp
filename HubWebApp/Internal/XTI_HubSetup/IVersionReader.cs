using XTI_Hub.Abstractions;

namespace XTI_HubSetup;

public interface IVersionReader
{
    Task<XtiVersionModel[]> Versions();
}
