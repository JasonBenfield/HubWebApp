import { AppRoleName } from "./AppRoleName";

export class AppRole {
    readonly id: number;
    readonly name: AppRoleName;

    constructor(source?: IAppRoleModel) {
        this.id = source ? source.ID : 0;
        this.name = new AppRoleName(source && source.Name);
    }
}