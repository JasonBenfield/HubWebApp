import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { InstallLocation } from "./InstallLocation";

export class AppCommandSummary {
    readonly command: AppCommand;
    readonly app: App;
    readonly location: InstallLocation;

    constructor(source?: IAppCommandSummaryModel) {
        this.command = new AppCommand(source && source.Command);
        this.app = new App(source && source.App);
        this.location = new InstallLocation(source && source.Location);
    }
}