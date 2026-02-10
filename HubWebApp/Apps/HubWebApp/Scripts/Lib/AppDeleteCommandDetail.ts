import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { AppCommandStep } from "./AppCommandStep";
import { Installation } from "./Installation";
import { XtiVersion } from "./XtiVersion";

export class AppDeleteCommandDetail {
	readonly command: AppCommand;
	readonly app: App;
	readonly version: XtiVersion;
	readonly steps: AppCommandStep[];
	readonly installation: Installation;

	constructor(source?: IAppDeleteCommandDetailModel) {
		this.command = new AppCommand(source && source.Command);
		this.app = new App(source && source.App);
		this.version = new XtiVersion(source && source.Version);
		this.steps = source ? source.Steps.map(s => new AppCommandStep(s)) : [];
		this.installation = new Installation(source && source.Installation);
    }
}