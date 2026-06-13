import { AppKey } from "./AppKey";
import { InstallConfigurationTemplate } from "./InstallConfigurationTemplate";

export class InstallConfiguration {
    readonly id: number;
    readonly repoOwner: string;
    readonly repoName: string;
    readonly configurationName: string;
    readonly appKey: AppKey;
    readonly template: InstallConfigurationTemplate;
    readonly installSequence: number;

    constructor(source?: IInstallConfigurationModel) {
        this.id = source?.ID || 0;
        this.repoOwner = source?.RepoOwner || "";
        this.repoName = source?.RepoName || "";
        this.configurationName = source?.ConfigurationName || "";
        this.appKey = new AppKey(source && source.AppKey);
        this.template = new InstallConfigurationTemplate(source && source.Template);
        this.installSequence = source?.InstallSequence || 0;
    }

    get isFound() { return this.id > 0; }
}