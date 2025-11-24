import { AppUserGroupName } from "./AppUserGroupName";
import { ModifierKey } from "./ModifierKey";

export class AppUserGroup {
	readonly id: number;
	readonly groupName: AppUserGroupName;
	readonly publicKey: ModifierKey;

	constructor(source?: IAppUserGroupModel) {
		this.id = source ? source.ID : 0;
		this.groupName = new AppUserGroupName(source && source.GroupName);
		this.publicKey = new ModifierKey(source && source.PublicKey);
	}

	getModifier() { return this.groupName.displayText.replace(/\s+/, ''); }
}