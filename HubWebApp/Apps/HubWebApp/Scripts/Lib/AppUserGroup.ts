import { AppUserGroupName } from "./AppUserGroupName";
import { ModifierKey } from "./ModifierKey";

export class AppUserGroup {
	readonly id: number;
	readonly groupName: AppUserGroupName;
	readonly publicKey: ModifierKey;

	constructor(source: IAppUserGroupModel) {
		this.id = source.ID;
		this.groupName = new AppUserGroupName(source.GroupName);
		this.publicKey = new ModifierKey(source.PublicKey);
	}

	getModifier() { return this.groupName.displayText.replace(/\s+/, ''); }
}