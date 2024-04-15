import { App } from "./App";
import { AppRequest } from "./AppRequest";
import { InstallLocation } from "./InstallLocation";
import { Installation } from "./Installation";
import { XtiVersion } from "./XtiVersion";

export class InstallationDetail {
	readonly installLocation: InstallLocation;
	readonly installation: Installation;
	readonly version: XtiVersion;
	readonly app: App;
	readonly mostRecentRequest: AppRequest;

	constructor(source: IInstallationDetailModel) {
		this.installLocation = new InstallLocation(source.InstallLocation);
		this.installation = new Installation(source.Installation);
		this.version = new XtiVersion(source.Version);
		this.app = new App(source.App);
		this.mostRecentRequest = new AppRequest(source.MostRecentRequest);
    }
}