import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";

export class AppRequest {
	readonly id: number;
	readonly sessionID: number;
	readonly path: string;
	readonly resourceID: number;
	readonly modifierID: number;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;

    constructor(source: IAppRequestModel) {
		this.id = source.ID;
		this.sessionID = source.SessionID;
		this.path = source.Path;
		this.resourceID = source.ResourceID;
		this.modifierID = source.ModifierID;
		this.timeStarted = source.TimeStarted;
		this.timeEnded = source.TimeEnded;
	}

	formatTimeRange() {

	}
}