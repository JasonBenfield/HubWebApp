// Generated Code
namespace HubWebApp.ApiControllers;
[Authorize]
[Route("odata/LogEntryQuery")]
public sealed partial class LogEntryQueryController : XtiODataController<LogEntryQueryRequest, ExpandedLogEntry>
{
    public LogEntryQueryController(HubAppApi api) : base(api.Group("LogEntryQuery"))
    {
    }
}