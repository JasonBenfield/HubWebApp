import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { HubAppApi } from "../../Lib/Api/HubAppApi";

export class UserGroupListItem extends BasicComponent {
    constructor(hubApi: HubAppApi, userGroup: IAppUserGroupModel, protected readonly view: TextLinkListGroupItemView) {
        super(view);
        if (userGroup === null) {
            new LinkComponent(view).setHref(
                hubApi.UserGroups.UserQuery.getUrl({ UserGroupName: '' })
            );
            new TextComponent(view).setText('All');
        }
        else {
            new LinkComponent(view).setHref(
                hubApi.UserGroups.UserQuery.getUrl({ UserGroupName: userGroup.GroupName.DisplayText })
            );
            new TextComponent(view).setText(userGroup.GroupName.DisplayText);
        }
    }

    makeActive() {
        this.view.active();
    }
}