import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { AppUserGroup } from "../../Lib/AppUserGroup";

export class UserGroupListItem extends TextComponent {
    constructor(readonly userGroup: AppUserGroup, protected readonly view: TextButtonListGroupItemView) {
        super(view);
        this.setText(userGroup.groupName.displayText);
    }

    makeActive() {
        this.view.active();
    }
}