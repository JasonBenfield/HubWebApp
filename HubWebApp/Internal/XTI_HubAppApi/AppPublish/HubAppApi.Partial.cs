using XTI_HubAppApi.AppPublish;

namespace XTI_HubAppApi;

partial class HubAppApi
{
    private PublishGroup? publish;

    public PublishGroup Publish { get => publish ?? throw new ArgumentNullException(nameof(publish)); }

    partial void createPublish(IServiceProvider sp)
    {
        publish = new PublishGroup
        (
            source.AddGroup(nameof(Publish)),
            sp
        );
    }
}