import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class RoleAccessListItem {
    constructor(accessItem: IRoleAccessItem, view: RoleAccessListItemView) {
        new TextBlock(accessItem.role.Name, view.roleName);
    }
}