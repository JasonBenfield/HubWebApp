import { InstallStatus } from "./Http/InstallStatus";

export class Installation {
	readonly id: number;
	readonly status: InstallStatus;
	readonly isCurrent: boolean;
	readonly domain: string;
	readonly siteName: string;

	constructor(source: IInstallationModel) {
		this.id = source.ID;
		this.status = InstallStatus.values.value(source.Status);
		this.isCurrent = source.IsCurrent;
		this.domain = source.Domain;
		this.siteName = source.SiteName;
    }
}