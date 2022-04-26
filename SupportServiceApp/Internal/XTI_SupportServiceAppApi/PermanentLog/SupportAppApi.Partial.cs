using XTI_SupportServiceAppApi.PermanentLog;

namespace XTI_SupportServiceAppApi;

partial class SupportAppApi
{
    private PermanentLogGroup? _PermanentLog;

    public PermanentLogGroup PermanentLog { get => _PermanentLog ?? throw new ArgumentNullException(nameof(_PermanentLog)); }

    partial void createPermanentLogGroup(IServiceProvider sp)
    {
        _PermanentLog = new PermanentLogGroup
        (
            source.AddGroup(nameof(PermanentLog)),
            sp
        );
    }
}