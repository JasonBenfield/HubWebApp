import { TextLinkComponent } from "@jasonbenfield/sharedwebapp/Components/TextLinkComponent";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { AppUserGroup } from "../../Lib/AppUserGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";

export class UserGroupListItem extends TextLinkComponent {
    constructor(hubClient: HubAppClient, readonly userGroup: AppUserGroup, protected readonly view: TextLinkListGroupItemView) {
        super(view);
        if (userGroup === null) {
            this.setHref(
                hubClient.UserGroups.UserQuery.getUrl({ UserGroupName: null })
            );
            this.setText("All");
        }
        else {
            this.setHref(
                hubClient.UserGroups.UserQuery.getUrl({
                    UserGroupName: userGroup.getModifier()
                })
            );
            this.setText(userGroup.groupName.displayText);
        }
    }

    makeActive() {
        this.view.active();
    }
}