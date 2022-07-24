import { WebPage } from "@jasonbenfield/sharedwebapp/Api/WebPage";
import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { ApiODataClient } from "@jasonbenfield/sharedwebapp/OData/ApiODataClient";
import { ODataCellClickedEventArgs } from "@jasonbenfield/sharedwebapp/OData/ODataCellClickedEventArgs";
import { ODataComponent } from "@jasonbenfield/sharedwebapp/OData/ODataComponent";
import { ODataComponentOptionsBuilder } from "@jasonbenfield/sharedwebapp/OData/ODataComponentOptionsBuilder";
import { TextLinkListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { ODataExpandedUserColumnsBuilder } from "../../Lib/Api/ODataExpandedUserColumnsBuilder";
import { UserGroupListItem } from "../UserGroups/UserGroupListItem";
import { UserQueryPanelView } from "./UserQueryPanelView";

interface IResult {
    menuRequested?: {};
    addRequested?: {};
}

class Result {
    static menuRequested() { return new Result({ menuRequested: {} }); }

    static addRequested() { return new Result({ addRequested: {} }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }

    get addRequested() { return this.result.addRequested; }
}

export class UserQueryPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly addCommand: Command;
    private readonly alert: MessageAlert;
    private readonly userGroups: ListGroup;
    private readonly odataComponent: ODataComponent<IExpandedUser>;
    private readonly userQueryModel: IUserGroupKey = { UserGroupName: '' };

    constructor(private readonly hubApi: HubAppApi, private readonly view: UserQueryPanelView) {
        this.alert = new MessageAlert(this.view.alert);
        this.userGroups = new ListGroup(this.view.userGroups);
        const columns = new ODataExpandedUserColumnsBuilder(this.view.columns);
        columns.UserID.require();
        columns.UserGroupName.require();
        const options = new ODataComponentOptionsBuilder<IExpandedUser>('hub_users', columns);
        options.query.select.addFields(
            columns.UserName,
            columns.PersonName,
            columns.Email
        );
        options.saveChanges();
        options.setODataClient(
            new ApiODataClient(this.hubApi.UserQuery, this.userQueryModel)
        );
        this.odataComponent = new ODataComponent(this.view.odataComponent, options.build());
        this.odataComponent.when.dataCellClicked.then(this.onDataCellClicked.bind(this));
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.addCommand = new Command(this.add.bind(this));
        this.addCommand.add(view.addButton);
        this.addCommand.hide();
    }

    setUserGroupName(userGroupName: string) {
        this.userQueryModel.UserGroupName = userGroupName;
        if (userGroupName) {
            const canAdd = this.canAdd(userGroupName);
            if (canAdd) {
                this.addCommand.show();
            }
            else {
                this.addCommand.hide();
            }
        }
        else {
            this.addCommand.hide();
        }
    }

    private async canAdd(userGroupName: string) {
        const permissions = await this.hubApi.getUserAccess({
            canAdd: this.hubApi.getAccessRequest(api => api.Users.AddUserAction, userGroupName)
        });
        return permissions.canAdd;
    }

    private onDataCellClicked(eventArgs: ODataCellClickedEventArgs) {
        const userID = eventArgs.record['UserID'];
        const userGroupName = eventArgs.record['UserGroupName'];
        this.hubApi.Users.Index.open({ UserID: userID }, userGroupName);
    }

    async refresh() {
        const userGroups = await this.getUserGroups();
        userGroups.splice(0, 0, null);
        this.userGroups.setItems(
            userGroups,
            (ug, itemView: TextLinkListGroupItemView) => {
                const listItem = new UserGroupListItem(this.hubApi, ug, itemView);
                const listItemGroupName = ug ? ug.GroupName.DisplayText : '';
                if (this.userQueryModel.UserGroupName === listItemGroupName) {
                    listItem.makeActive();
                }
                return listItem;
            }
        );
        this.odataComponent.refresh();
    }

    private getUserGroups() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubApi.UserGroups.GetUserGroups()
        );
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    private add() { this.awaitable.resolve(Result.addRequested()); }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}