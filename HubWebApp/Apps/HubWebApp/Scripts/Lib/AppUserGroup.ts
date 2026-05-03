import { AppUserGroupName } from "./AppUserGroupName";
import { ModifierKey } from "./ModifierKey";

export class AppUserGroup {
    readonly id: number;
    readonly groupName: AppUserGroupName;
    readonly publicKey: ModifierKey;

    constructor(source?: IAppUserGroupModel) {
        this.id = source?.ID || 0;
        this.groupName = new AppUserGroupName(source && source.GroupName);
        this.publicKey = new ModifierKey(source && source.PublicKey);
    }

    get isFound() { return this.id > 0; }

    getModifier() { return this.groupName.displayText.replace(/\s+/, ''); }
}