import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";

export class AppSession {
	readonly id: number;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;
	readonly remoteAddress: string;
	readonly userAgent: string;

	constructor(source: IAppSessionModel) {
		this.id = source.ID;
		this.timeStarted = source.TimeStarted;
		this.timeEnded = source.TimeEnded;
		this.remoteAddress = source.RemoteAddress;
		this.userAgent = source.UserAgent;
    }
}