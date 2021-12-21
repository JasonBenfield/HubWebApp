import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { SimpleEvent } from "@jasonbenfield/sharedwebapp/Events";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { RoleListItem } from "./RoleListItem";
import { RoleListItemView } from "./RoleListItemView";
import { UserRoleListCardView } from "./UserRoleListCardView";

export class UserRoleListCard {
    private readonly alert: MessageAlert;
    private readonly roles: ListGroup;

    private userID: number;

    private readonly _editRequested = new SimpleEvent(this);
    readonly editRequested = this._editRequested.handler();

    private readonly editCommand: Command;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserRoleListCardView
    ) {
        this.editCommand = new Command(this.requestEdit.bind(this));
        this.editCommand.add(this.view.editButton);
        this.alert = new MessageAlert(this.view.alert);
        this.roles = new ListGroup(this.view.roles);
    }

    private requestEdit() {
        this._editRequested.invoke();
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let roles = await this.getRoles();
        this.roles.setItems(
            roles,
            (role: IAppRoleModel, listItem: RoleListItemView) =>
                new RoleListItem(role, listItem)
        );
        if (roles.length === 0) {
            this.alert.danger('No Roles were Found for User');
        }
    }

    private async getRoles() {
        let roles: IAppRoleModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                roles = await this.hubApi.AppUser.GetUserRoles({
                    UserID: this.userID,
                    ModifierID: 0
                });
            }
        );
        return roles;
    }
}