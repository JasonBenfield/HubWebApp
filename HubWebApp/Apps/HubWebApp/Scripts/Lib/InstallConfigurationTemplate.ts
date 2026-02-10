
export class InstallConfigurationTemplate {
	readonly id: number;
	readonly templateName: string;
	readonly destinationMachineName: string;
	readonly domain: string;
	readonly siteName: string;

	constructor(source?: IInstallConfigurationTemplateModel) {
		this.id = source ? source.ID : 0;
		this.templateName = source ? source.TemplateName : "";
		this.destinationMachineName = source ? source.DestinationMachineName : "";
		this.domain = source ? source.Domain : "";
		this.siteName = source ? source.SiteName : "";
    }
}