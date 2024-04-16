import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/DateTimeOffset";
import { JoinedStrings } from "@jasonbenfield/sharedwebapp/JoinedStrings";
import * as Bowser from "bowser";

export class AppSession {
	readonly id: number;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;
	readonly remoteAddress: string;
	readonly rawUserAgent: string;
	readonly userAgent: string;

	constructor(source: IAppSessionModel) {
		this.id = source.ID;
		this.timeStarted = source.TimeStarted;
		this.timeEnded = source.TimeEnded;
		this.remoteAddress = source.RemoteAddress;
		this.rawUserAgent = source.UserAgent;
		if (source.UserAgent) {
			const parsedUA = Bowser.getParser(source.UserAgent);
			const browser = parsedUA.getBrowser();
			const engine = parsedUA.getEngine();
			const platform = parsedUA.getPlatform();
			const os = parsedUA.getOS();
			const line1 = new JoinedStrings(' ', [browser.name, browser.version, engine.name, engine.version].filter(str => Boolean(str))).value();
			const line2 = new JoinedStrings(' ', [platform.vendor, platform.model, platform.type, os.name, os.version].filter(str => Boolean(str))).value();
			this.userAgent = `${line1}\r\n${line2}`.trim();
		}
		else {
			this.userAgent = '';
		}
    }
}