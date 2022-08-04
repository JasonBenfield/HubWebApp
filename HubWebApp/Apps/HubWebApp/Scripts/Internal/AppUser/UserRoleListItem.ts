import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { UserRoleListItemView } from "./UserRoleListItemView";

export class UserRoleListItem extends BasicComponent {
    constructor(role: IAppRoleModel, protected readonly view: UserRoleListItemView) {
        super(view);
        new TextComponent(view.roleName).setText(role.Name.DisplayText);
    }

    hideDeleteButton() {
        this.view.deleteButton.hide();
    }
}