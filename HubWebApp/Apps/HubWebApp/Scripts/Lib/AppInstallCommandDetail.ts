import { App } from "./App";
import { AppCommand } from "./AppCommand";
import { AppCommandStep } from "./AppCommandStep";
import { IAppCommandDetail } from "./IAppCommandDetail";
import { Installation } from "./Installation";
import { InstallConfiguration } from "./InstallConfiguration";
import { InstallLocation } from "./InstallLocation";
import { XtiVersion } from "./XtiVersion";

export class AppInstallCommandDetail implements IAppCommandDetail {
	readonly command: AppCommand;
	readonly app: App;
	readonly location: InstallLocation;
	readonly steps: AppCommandStep[];
	readonly installRequest: IAddInstallCommandRequest;
	readonly version: XtiVersion;
	readonly installConfiguration: InstallConfiguration;
	readonly installations: Installation[];

	constructor(source?: IAppInstallCommandDetailModel) {
		this.command = new AppCommand(source && source.Command);
		this.app = new App(source && source.App);
		this.location = new InstallLocation(source && source.Location);
		this.steps = source ? source.Steps.map(s => new AppCommandStep(s)) : [];
		this.installRequest = source?.InstallRequest || {
			AppKey: { AppName: "", AppType: 0 },
			VersionKey: "",
			InstallAsCurrent: false,
			IsAutoStartEnabled: false,
			InstallConfigurationID: 0
		};
		this.version = new XtiVersion(source && source.Version);
		this.installConfiguration = new InstallConfiguration(source && source.InstallConfiguration);
		this.installations = source?.Installations.map(inst => new Installation(inst)) || [];
	}

	getCurrentInstallationOrDefault() {
		return this.installations.find(inst => inst.isCurrent) || new Installation();
	}

	getVersionInstallationOrDefault() {
		return this.installations.find(inst => !inst.isCurrent) || new Installation();
	}
}