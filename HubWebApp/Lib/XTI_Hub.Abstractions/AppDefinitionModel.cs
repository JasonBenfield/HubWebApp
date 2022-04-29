using XTI_App.Abstractions;

namespace XTI_Hub.Abstractions;

public sealed record AppDefinitionModel(AppKey AppKey)
{
    public AppDefinitionModel()
        :this(AppKey.Unknown)
    {
    }
}
