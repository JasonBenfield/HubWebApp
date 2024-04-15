import { AppKey } from "./AppKey";
import { AppVersionName } from "./AppVersionName";
import { ModifierKey } from "./ModifierKey";

export class App {
	readonly id: number;
	readonly appKey: AppKey;
	readonly versionName: AppVersionName;
	readonly publicKey: ModifierKey;

    constructor(readonly source: IAppModel) {
		this.id = source.ID;
		this.appKey = new AppKey(source.AppKey);
		this.versionName = new AppVersionName(source.VersionName);
		this.publicKey = new ModifierKey(source.PublicKey);
	}

	getModifier() {
		return this.publicKey.displayText.replace(/\s+/g, '');
	}
}