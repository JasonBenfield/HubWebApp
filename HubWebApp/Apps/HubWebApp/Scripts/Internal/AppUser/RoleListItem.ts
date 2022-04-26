import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { RoleButtonListItemView } from "./RoleButtonListItemView";

export class RoleListItem {
    constructor(readonly role: IAppRoleModel, view: RoleButtonListItemView) {
        new TextBlock(role.Name, view.roleName);
    }
} 