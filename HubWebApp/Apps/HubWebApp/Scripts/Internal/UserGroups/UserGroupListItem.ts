import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppUserGroup } from "../../Lib/AppUserGroup";

export class UserGroupListItem extends BasicComponent {
    constructor(hubClient: HubAppClient, userGroup: AppUserGroup, protected readonly view: TextLinkListGroupItemView) {
        super(view);
        if (userGroup === null) {
            new LinkComponent(view).setHref(
                hubClient.UserGroups.UserQuery.getUrl({ UserGroupName: '' })
            );
            new TextComponent(view).setText('All');
        }
        else {
            new LinkComponent(view).setHref(
                hubClient.UserGroups.UserQuery.getUrl({
                    UserGroupName: userGroup.getModifier()
                })
            );
            new TextComponent(view).setText(userGroup.groupName.displayText);
        }
    }

    makeActive() {
        this.view.active();
    }
}