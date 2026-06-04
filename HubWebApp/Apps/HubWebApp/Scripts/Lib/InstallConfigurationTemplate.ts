
export class InstallConfigurationTemplate {
	readonly id: number;
	readonly templateName: string;
	readonly destinationMachineName: string;
	readonly domain: string;
	readonly siteName: string;

	constructor(source?: IInstallConfigurationTemplateModel) {
		this.id = source?.ID || 0;
		this.templateName = source?.TemplateName || "";
		this.destinationMachineName = source?.DestinationMachineName || "";
		this.domain = source?.Domain || "";
		this.siteName = source?.SiteName || "";
	}

	get isFound() { return this.id > 0; }
}