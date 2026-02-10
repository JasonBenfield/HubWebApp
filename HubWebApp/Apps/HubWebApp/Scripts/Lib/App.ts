import { AppKey } from "./AppKey";
import { AppVersionName } from "./AppVersionName";
import { ModifierKey } from "./ModifierKey";

export class App {
	readonly id: number;
	readonly appKey: AppKey;
	readonly versionName: AppVersionName;
	readonly repoOwner: string;
	readonly repoName: string;
	readonly publicKey: ModifierKey;

    constructor(source?: IAppModel) {
		this.id = source ? source.ID : 0;
		this.appKey = new AppKey(source && source.AppKey);
		this.repoOwner = source ? source.RepoOwner : "";
		this.repoName = source ? source.RepoName : "";
		this.versionName = new AppVersionName(source && source.VersionName);
		this.publicKey = new ModifierKey(source && source.PublicKey);
	}

	getModifier() {
		return this.publicKey.displayText.replace(/\s+/g, '');
	}
}