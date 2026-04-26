import { DateTimeOffset } from "@jasonbenfield/sharedwebapp/Common";
import { AppCommandName } from "./AppCommandName";
import { AppCommandStatus } from "./Http/AppCommandStatus";

export class AppCommand {
    readonly id: number;
    readonly commandName: AppCommandName;
    readonly status: AppCommandStatus;
    readonly timeAdded: DateTimeOffset;
    readonly timeStarted: DateTimeOffset;
    readonly timeEnded: DateTimeOffset;

    constructor(source?: IAppCommandModel) {
        this.id = source?.ID || 0;
        this.commandName = new AppCommandName(source && source.CommandName);
        this.status = source ? AppCommandStatus.values.value(source.Status) : AppCommandStatus.values.NotSet;
        this.timeAdded = source?.TimeAdded || DateTimeOffset.max();
        this.timeStarted = source?.TimeStarted || DateTimeOffset.max();
        this.timeEnded = source?.TimeEnded || DateTimeOffset.max();
    }

    get isFound() { return this.id > 0; }

    get isPending() { return this.status.equals(AppCommandStatus.values.Pending); }
    get isInProgress() { return this.status.equals(AppCommandStatus.values.Started); }
    get isCancelled() { return this.status.equals(AppCommandStatus.values.Cancelled); }
    get isComplete() { return this.status.equals(AppCommandStatus.values.Completed); }
    get isFailed() { return this.status.equals(AppCommandStatus.values.Failed); }
}