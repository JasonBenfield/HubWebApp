import { RoleListItemView } from "./RoleListItemView";

export class RoleListItem {
    constructor(role: IAppRoleModel, view: RoleListItemView) {
        view.setRoleName(role.Name);
    }
}