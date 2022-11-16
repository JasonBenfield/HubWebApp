using XTI_HubDB.Entities;
using XTI_HubWebAppApi.Installations;
using XTI_ODataQuery.Api;

namespace XTI_HubWebAppApi;

partial class HubAppApi
{
    private InstallationsGroup? _Installations;

    public InstallationsGroup Installations { get => _Installations ?? throw new ArgumentNullException(nameof(_Installations)); }

    private ODataGroup<InstallationQueryRequest, ExpandedInstallation>? _InstallationQuery;

    public ODataGroup<InstallationQueryRequest, ExpandedInstallation> InstallationQuery
    {
        get => _InstallationQuery ?? throw new ArgumentNullException(nameof(InstallationQuery));
    }

    partial void createInstallationsGroup(IServiceProvider sp)
    {
        _Installations = new InstallationsGroup
        (
            source.AddGroup(nameof(Installations)),
            sp
        );
        _InstallationQuery = new ODataGroup<InstallationQueryRequest, ExpandedInstallation>
        (
            source.AddGroup(nameof(InstallationQuery)),
            () => sp.GetRequiredService<InstallationQueryAction>()
        );
    }
}