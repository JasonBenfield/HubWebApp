import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { AppUserGroup } from "../../Lib/AppUserGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { HubPermissions, IHubPermissions } from "../../Lib/HubPermissions";
import { UserGroupListItem } from "./UserGroupListItem";
import { UserGroupsPanelView } from "./UserGroupsPanelView";

interface IResult {
    addRequested?: boolean;
    mainMenuRequested?: boolean;
}

class Result {
    static addRequested() { return new Result({ addRequested: true }); }

    static mainMenuRequested() { return new Result({ mainMenuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get addRequested() { return this.result.addRequested; }

    get mainMenuRequested() { return this.result.mainMenuRequested; }
}

export class UserGroupsPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly userGroups: ListGroup<UserGroupListItem, TextLinkListGroupItemView>;
    private readonly refreshCommand: AsyncCommand;
    private readonly addCommand: Command;
    private permissions: IHubPermissions | null = null;

    constructor(private readonly hubClient: HubAppClient, private readonly view: UserGroupsPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.userGroups = new ListGroup(view.userGroupListView);
        this.refreshCommand = new AsyncCommand(this._refresh.bind(this));
        this.refreshCommand.add(view.refreshButton);
        this.refreshCommand.animateIconWhenInProgress("spin");
        new Command(this.requestMainMenu.bind(this)).add(view.menuButton);
        this.addCommand = new Command(this.requestAdd.bind(this));
        this.addCommand.add(view.addButton);
        this.addCommand.hide();
    }

    private requestMainMenu() { this.awaitable.resolve(Result.mainMenuRequested()); }

    private requestAdd() { this.awaitable.resolve(Result.addRequested()); }

    refresh() { return this.refreshCommand.execute(); }

    private async _refresh() {
        if (!this.permissions) {
            this.permissions = await this.alert.infoAction(
                "Loading...",
                () => new HubPermissions(this.hubClient).value()
            );
            if (this.permissions.canAddUserGroup) {
                this.addCommand.show();
            }
        }
        const sourceUserGroups = await this.getUserGroups();
        const userGroups = sourceUserGroups.map(ug => new AppUserGroup(ug));
        userGroups.splice(0, 0, new AppUserGroup());
        this.userGroups.setItems(
            userGroups,
            (ug, itemView) => new UserGroupListItem(this.hubClient, ug, itemView)
        );
    }

    private getUserGroups() {
        return this.alert.infoAction(
            "Loading...",
            () => this.hubClient.UserGroups.GetUserGroups()
        );
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}