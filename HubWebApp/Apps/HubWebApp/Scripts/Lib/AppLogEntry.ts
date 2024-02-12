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

    constructor(readonly source: IAppLogEntryModel) {
        this.id = source.ID;
        this.requestID = source.RequestID;
        this.timeOccurred = source.TimeOccurred;
        this.severity = AppEventSeverity.values.value(source.Severity);
        this.caption = source.Caption;
        this.message = source.Message;
        this.detail = source.Detail;
        this.category = source.Category;
    }
}