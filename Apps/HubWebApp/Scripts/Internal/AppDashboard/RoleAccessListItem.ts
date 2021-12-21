import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class RoleAccessListItem {
    constructor(accessItem: IRoleAccessItem, view: RoleAccessListItemView) {
        if (accessItem.isAllowed) {
            view.allowAccess();
        }
        else {
            view.denyAccess();
        }
        view.setRoleName(accessItem.role.Name);
    }
}