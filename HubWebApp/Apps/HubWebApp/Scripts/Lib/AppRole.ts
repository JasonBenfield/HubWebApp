import { AppRoleName } from "./AppRoleName";

export class AppRole {
    readonly id: number;
    readonly name: AppRoleName;

    constructor(readonly source: IAppRoleModel) {
        this.id = source.ID;
        this.name = new AppRoleName(source.Name);
    }
}