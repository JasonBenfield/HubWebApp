import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserRoleListItem } from "./EditUserRoleListItem";
import { EditUserRoleListItemView } from "./EditUserRoleListItemView";
import { UserRolePanelView } from "./UserRolePanelView";

interface Results {
    backRequested?: {};
}

export class UserRolePanelResult {
    static get backRequested() {
        return new UserRolePanelResult({ backRequested: {} });
    }

    private constructor(private readonly results: Results) {
    }

    get backRequested() { return this.results.backRequested; }
}

export class UserRolePanel implements IPanel {
    private readonly alert: MessageAlert;
    private readonly userRoles: ListGroup;
    private userID: number;
    private readonly awaitable = new Awaitable<UserRolePanelResult>();
    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserRolePanelView
    ) {
        new CardTitleHeader('Edit User Roles', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.userRoles = new ListGroup(this.view.userRoles);
        this.backCommand.add(this.view.backButton);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let access = await this.getUserRoleAccess(this.userID);
        let sourceItems: IAppRoleModel[] = [];
        for (let userRole of access.AssignedRoles) {
            sourceItems.push(userRole);
        }
        for (let role of access.UnassignedRoles) {
            sourceItems.push(role);
        }
        sourceItems.sort(this.compare.bind(this));
        this.userRoles.setItems(
            sourceItems,
            (role: IAppRoleModel, view: EditUserRoleListItemView) => {
                let listItem = new EditUserRoleListItem(this.hubApi, view);
                listItem.setUserID(this.userID);
                listItem.withAssignedRole(role);
            }
        );
    }

    private compare(a: IAppRoleModel, b: IAppRoleModel) {
        let roleName: string;
        roleName = a.Name;
        let otherRoleName: string;
        otherRoleName = b.Name;
        let result: number;
        if (roleName < otherRoleName) {
            result = -1;
        }
        else if (roleName === otherRoleName) {
            result = 0;
        }
        else {
            result = 1;
        }
        return result;
    }

    private async getUserRoleAccess(userID: number) {
        let access: IUserRoleAccessModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                let request: IGetUserRoleAccessRequest = {
                    UserID: userID,
                    ModifierID: 0
                };
                access = await this.hubApi.AppUser.GetUserRoleAccess(request);
            }
        );
        return access;
    }

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(UserRolePanelResult.backRequested);
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}