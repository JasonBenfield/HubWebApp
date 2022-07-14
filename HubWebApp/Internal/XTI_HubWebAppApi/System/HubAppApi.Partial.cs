using XTI_HubWebAppApi.System;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private SystemGroup? _System;

    public SystemGroup System { get => _System ?? throw new ArgumentNullException(nameof(_System)); }

    partial void createSystemGroup(IServiceProvider sp)
    {
        _System = new SystemGroup
        (
            source.AddGroup(nameof(System), Access.WithAllowed(AppRoleName.System)),
            sp
        );
    }
}