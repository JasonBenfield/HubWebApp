import { InstallStatus } from "./Http/InstallStatus";

export class Installation {
    readonly id: number;
    readonly status: InstallStatus;
    readonly isCurrent: boolean;
    readonly domain: string;
    readonly siteName: string;

    constructor(source?: IInstallationModel) {
        this.id = source ? source.ID : 0;
        this.status = source ? InstallStatus.values.value(source.Status) : InstallStatus.values.NotSet;
        this.isCurrent = source ? source.IsCurrent : false;
        this.domain = source ? source.Domain : "";
        this.siteName = source ? source.SiteName : "";
    }

    get isFound() { return this.id > 0; }
}