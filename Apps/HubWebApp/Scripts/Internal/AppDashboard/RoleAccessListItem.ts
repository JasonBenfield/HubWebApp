import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class RoleAccessListItem {
    constructor(accessItem: IRoleAccessItem, view: RoleAccessListItemView) {
        if (accessItem.isAllowed) {
            view.allowAccess();
        }
        else {
            view.denyAccess();
        }
        new TextBlock(accessItem.role.Name, view.roleName);
    }
}