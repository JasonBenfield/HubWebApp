// Generated Code
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace HubWebApp.ApiControllers;
public sealed partial class EdmModelBuilder
{
    private readonly ODataConventionModelBuilder odataBuilder = new();
    public EdmModelBuilder()
    {
        UserQuery = odataBuilder.EntitySet<ExpandedUser>("UserQuery");
        SessionQuery = odataBuilder.EntitySet<ExpandedSession>("SessionQuery");
        RequestQuery = odataBuilder.EntitySet<ExpandedRequest>("RequestQuery");
        LogEntryQuery = odataBuilder.EntitySet<ExpandedLogEntry>("LogEntryQuery");
        init();
    }

    partial void init();
    public EntitySetConfiguration<ExpandedUser> UserQuery { get; }

    public EntitySetConfiguration<ExpandedSession> SessionQuery { get; }

    public EntitySetConfiguration<ExpandedRequest> RequestQuery { get; }

    public EntitySetConfiguration<ExpandedLogEntry> LogEntryQuery { get; }

    public IEdmModel GetEdmModel() => odataBuilder.GetEdmModel();
}