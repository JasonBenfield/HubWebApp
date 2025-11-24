import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";
import { AppEventSeverity } from "./Http/AppEventSeverity";

export class AppLogEntry {
    readonly id: number;
    readonly requestID: number;
    readonly timeOccurred: DateTimeOffset;
    readonly severity: AppEventSeverity;
    readonly caption: string;
    readonly message: string;
    readonly detail: string;
    readonly category: string;
    readonly actualCount: number;

    constructor(readonly source?: IAppLogEntryModel) {
        this.id = source ? source.ID : 0;
        this.requestID = source ? source.RequestID : 0;
        this.timeOccurred = source ? source.TimeOccurred : DateTimeOffset.max();
        this.severity = source ? AppEventSeverity.values.value(source.Severity) : AppEventSeverity.values.NotSet;
        this.caption = source ? source.Caption : "";
        this.message = source ? source.Message : "";
        this.detail = source ? source.Detail : "";
        this.category = source ? source.Category : "";
        this.actualCount = source ? source.ActualCount : 0;
    }
}