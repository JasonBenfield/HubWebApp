using XTI_SupportServiceAppApi.Installations;

namespace XTI_SupportServiceAppApi;

partial class SupportAppApi
{
    private InstallationsGroup? _Installations;

    public InstallationsGroup Installations { get => _Installations ?? throw new ArgumentNullException(nameof(_Installations)); }

    partial void createInstallationsGroup(IServiceProvider sp)
    {
        _Installations = new InstallationsGroup
        (
            source.AddGroup(nameof(Installations)),
            sp
        );
    }
}