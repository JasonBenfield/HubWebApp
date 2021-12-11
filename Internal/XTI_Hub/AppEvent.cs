using XTI_HubDB.Entities;
using XTI_App.Abstractions;
using XTI_Core;

namespace XTI_Hub
{
    public sealed class AppEvent
    {
        private readonly AppEventEntity record;

        internal AppEvent(AppEventEntity record)
        {
            this.record = record ?? new AppEventEntity();
            ID = new EntityID(this.record.ID);
        }

        public EntityID ID { get; }
        public string Caption { get => record.Caption; }
        public string Message { get => record.Message; }
        public string Detail { get => record.Detail; }
        public AppEventSeverity Severity() => AppEventSeverity.Values.Value(record.Severity);

        public AppEventModel ToModel() => new AppEventModel
        {
            ID = ID.Value,
            RequestID = record.RequestID,
            TimeOccurred = record.TimeOccurred,
            Severity = Severity(),
            Caption = Caption,
            Message = Message,
            Detail = Detail
        };

        public override string ToString() => $"{nameof(AppEvent)} {ID.Value}";
    }
}
