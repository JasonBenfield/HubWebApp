namespace XTI_HubWebAppApi.PermanentLog;

partial class PermanentLogGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.PermanentLog);
    }
}
