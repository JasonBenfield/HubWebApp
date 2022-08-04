import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { RoleAccessListItemView } from "./RoleAccessListItemView";

export class RoleAccessListItem extends BasicComponent {
    constructor(accessItem: IRoleAccessItem, view: RoleAccessListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(accessItem.role.Name.DisplayText);
    }
}