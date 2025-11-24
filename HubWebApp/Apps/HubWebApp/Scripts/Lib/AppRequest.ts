import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";

export class AppRequest {
    readonly id: number;
    readonly path: string;
    readonly resourceID: number;
    readonly modifierID: number;
    readonly timeStarted: DateTimeOffset;
    readonly timeEnded: DateTimeOffset;
    readonly actualCount: number;

    constructor(source?: IAppRequestModel) {
        this.id = source ? source.ID : 0;
        this.path = source ? source.Path : "";
        this.resourceID = source ? source.ResourceID : 0;
        this.modifierID = source ? source.ModifierID : 0;
        this.timeStarted = source ? source.TimeStarted : DateTimeOffset.max();
        this.timeEnded = source ? source.TimeEnded : DateTimeOffset.max();
        this.actualCount = source ? source.ActualCount : 0;
    }
}