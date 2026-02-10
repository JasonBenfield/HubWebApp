import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/Common";

export class AppCommandStep {
	readonly id: number;
	readonly activity: string;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;
	readonly errorMessage: string;

    constructor(source?: IAppCommandStepModel) {
		this.id = source ? source.ID : 0;
		this.activity = source ? source.Activity : "";
		this.timeStarted = source ? source.TimeStarted : DateTimeOffset.max();
		this.timeEnded = source ? source.TimeEnded : DateTimeOffset.max();
		this.errorMessage = source ? source.ErrorMessage : "";
    }
}