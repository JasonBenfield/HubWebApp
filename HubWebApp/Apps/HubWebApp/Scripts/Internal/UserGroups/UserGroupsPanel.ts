import { Awaitable } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Awaitable";
import { AsyncCommand } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/Command";
import { ListGroup } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/ListGroup";
import { MessageAlert } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Components/MessageAlert";
import { TextLinkListGroupItemView } from "../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ListGroup";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { UserGroupListItem } from "./UserGroupListItem";
import { UserGroupsPanelView } from "./UserGroupsPanelView";

interface IResult {
    addRequested?: {};
}

class Result {
    static addRequested() { return new Result({ addRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get addRequested() { return this.result.addRequested; }
}

export class UserGroupsPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly userGroups: ListGroup;
    private readonly refreshCommand: AsyncCommand;

    constructor(private readonly hubApi: HubAppApi, private readonly view: UserGroupsPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.userGroups = new ListGroup(view.userGroups);
        this.refreshCommand = new AsyncCommand(this._refresh.bind(this));
        this.refreshCommand.add(view.refreshButton);
        this.refreshCommand.animateIconWhenInProgress('spin');
    }

    refresh() { return this.refreshCommand.execute(); }

    private async _refresh() {
        const userGroups = await this.getUserGroups();
        userGroups.splice(0, 0, null);
        this.userGroups.setItems(
            userGroups,
            (ug, itemView: TextLinkListGroupItemView) => new UserGroupListItem(this.hubApi, ug, itemView)
        );
    }

    private getUserGroups() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.UserGroups.GetUserGroups()
        );
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}