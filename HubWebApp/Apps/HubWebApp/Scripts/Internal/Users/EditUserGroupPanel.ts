import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { XtiUrl } from "@jasonbenfield/sharedwebapp/Http/XtiUrl";
import { TextButtonListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { AppUserGroup } from "../../Lib/AppUserGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { EditUserGroupPanelView } from "./EditUserGroupPanelView";
import { UserGroupListItem } from "./UserGroupListItem";

interface IResults {
    backRequested?: boolean;
    userGroupChanged?: { userID: number, userGroup: AppUserGroup };
}

class Result {
    static backRequested() { return new Result({ backRequested: true }); }

    static userGroupChanged(userID: number, userGroup: AppUserGroup) {
        return new Result({ userGroupChanged: { userID: userID, userGroup: userGroup } });
    }

    private constructor(private readonly results: IResults) { }

    get backRequested() { return this.results.backRequested; }

    get userGroupChanged() { return this.results.userGroupChanged; }
}

export class EditUserGroupPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly userGroupListGroup: ListGroup<UserGroupListItem, TextButtonListGroupItemView>;
    private userID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: EditUserGroupPanelView
    ) {
        this.alert = new MessageAlert(view.alertView);
        this.userGroupListGroup = new ListGroup(view.userGroupListView);
        this.userGroupListGroup.when.itemClicked.then(this.onUserGroupSelected.bind(this));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private async onUserGroupSelected(listItem: UserGroupListItem) {
        await this.alert.infoAction(
            "Updating...",
            () => this.hubClient.AppUserMaintenance.EditUserGroup({
                UserID: this.userID,
                UserGroupID: listItem.userGroup.id
            })
        );
        this.awaitable.resolve(Result.userGroupChanged(this.userID, listItem.userGroup));
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        const sourceUserGroups = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.UserGroups.GetUserGroups()
        );
        const userGroups = sourceUserGroups.map(ug => new AppUserGroup(ug));
        this.userGroupListGroup.setItems(
            userGroups,
            (ug, itemView) => new UserGroupListItem(ug, itemView)
        );
        const userGroup = XtiUrl.current().path.modifier.toLowerCase();
        for (const listItem of this.userGroupListGroup.getItems()) {
            if (listItem.userGroup.publicKey.displayText.toLowerCase() === userGroup) {
                listItem.makeActive();
            }
        }
    }

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(Result.backRequested());
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}