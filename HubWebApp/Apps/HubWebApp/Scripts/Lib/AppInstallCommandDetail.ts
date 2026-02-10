import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { AppCommandStep } from "./AppCommandStep";
import { Installation } from "./Installation";
import { InstallConfiguration } from "./InstallConfiguration";
import { InstallLocation } from "./InstallLocation";
import { XtiVersion } from "./XtiVersion";

export class AppInstallCommandDetail {
	readonly command: AppCommand;
	readonly installRequest: IAddInstallCommandRequest;
	readonly app: App;
	readonly version: XtiVersion;
	readonly location: InstallLocation;
	readonly installConfiguration: InstallConfiguration;
	readonly steps: AppCommandStep[];
	readonly installations: Installation[];

	constructor(source?: IAppInstallCommandDetailModel) {
		this.command = new AppCommand(source && source.Command);
		this.installRequest = source && source.InstallRequest;
		this.app = new App(source && source.App);
		this.version = new XtiVersion(source && source.Version);
		this.location = new InstallLocation(source && source.Location);
		this.installConfiguration = new InstallConfiguration(source && source.InstallConfiguration);
		this.steps = source ? source.Steps.map(s => new AppCommandStep(s)) : [];
		this.installations = source ? source.Installations.map(inst => new Installation(inst)) : [];
    }
}