import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { UserListItemView } from "./UserListItemView";

export class UserListItem extends BasicComponent {
    constructor(readonly user: IAppUserModel, view: UserListItemView) {
        super(view);
        new TextComponent(view.userName).setText(user.UserName.DisplayText);
        new TextComponent(view.fullName).setText(user.Name.DisplayText);
    }
}
