import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/Common";
import { AppCommandStatus } from "./Http/AppCommandStatus";
import { AppCommandName } from "./AppCommandName";

export class AppCommand {
	readonly id: number;
	readonly commandName: AppCommandName;
	readonly status: AppCommandStatus;
	readonly timeAdded: DateTimeOffset;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;

    constructor(source?: IAppCommandModel) {
		this.id = source ? source.ID : 0;
		this.commandName = new AppCommandName(source && source.CommandName);
		this.status = source ? AppCommandStatus.values.value(source.Status) : AppCommandStatus.values.NotSet;
		this.timeAdded = source ? source.TimeAdded : DateTimeOffset.max();
		this.timeStarted = source ? source.TimeStarted : DateTimeOffset.max();
		this.timeEnded = source ? source.TimeEnded : DateTimeOffset.max();
	}

	get isPending() { return this.status.equals(AppCommandStatus.values.Pending); }
	get isInProgress() { return this.status.equals(AppCommandStatus.values.Started); }
	get isComplete() { return this.status.equals(AppCommandStatus.values.Completed); }
	get isFailed() { return this.status.equals(AppCommandStatus.values.Failed); }
}