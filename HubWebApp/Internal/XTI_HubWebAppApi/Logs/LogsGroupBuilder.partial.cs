namespace XTI_HubWebAppApi.Logs;

partial class LogsGroupBuilder
{
    partial void Configure()
    {
        source.WithAllowed(HubInfo.Roles.ViewLog);
    }
}
