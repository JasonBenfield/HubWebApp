import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { AppCommandStep } from "./AppCommandStep";
import { IAppCommandDetail } from "./IAppCommandDetail";
import { Installation } from "./Installation";
import { InstallLocation } from "./InstallLocation";
import { XtiVersion } from "./XtiVersion";

export class AppDeleteCommandDetail implements IAppCommandDetail {
	readonly command: AppCommand;
	readonly app: App;
	readonly location: InstallLocation;
	readonly steps: AppCommandStep[];
	readonly version: XtiVersion;
	readonly installation: Installation;

	constructor(source?: IAppDeleteCommandDetailModel) {
		this.command = new AppCommand(source && source.Command);
		this.app = new App(source && source.App);
		this.location = new InstallLocation(source && source.Location);
		this.steps = source ? source.Steps.map(s => new AppCommandStep(s)) : [];
		this.version = new XtiVersion(source && source.Version);
		this.installation = new Installation(source && source.Installation);
    }
}