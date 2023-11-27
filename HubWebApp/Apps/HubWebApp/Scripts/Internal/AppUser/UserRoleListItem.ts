import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { UserRoleListItemView } from "./UserRoleListItemView";
import { AppRole } from "../../Lib/AppRole";

export class UserRoleListItem extends BasicComponent {
    constructor(readonly role: AppRole, protected readonly view: UserRoleListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(role.name.displayText);
    }

    hideDeleteButton() {
        this.view.deleteButton.hide();
    }
}