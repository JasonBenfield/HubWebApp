import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DropDownFormGroup } from "@jasonbenfield/sharedwebapp/Forms/DropDownFormGroup";
import { SelectOption } from "@jasonbenfield/sharedwebapp/Html/SelectOption";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserModifierListItem } from "./EditUserModifierListItem";
import { EditUserModifierListItemView } from "./EditUserModifierListItemView";
import { UserModCategoryPanelView } from "./UserModCategoryPanelView";

interface Results {
    backRequested?: {};
}

export class UserModCategoryPanelResult {
    static get backRequested() { return new UserModCategoryPanelResult({ backRequested: {} }); }

    private constructor(private readonly results: Results) { }

    get backRequested() { return this.results.backRequested; }
}

export class UserModCategoryPanel {
    private readonly alert: MessageAlert;
    private readonly userModifiers: ListGroup;
    private readonly hasAccessToAll: DropDownFormGroup<boolean>;
    private userID: number;
    private readonly awaitable = new Awaitable<UserModCategoryPanelResult>();
    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserModCategoryPanelView
    ) {
        new CardTitleHeader('Edit User Modifiers', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.hasAccessToAll = new DropDownFormGroup<boolean>('', 'HasAccessToAll', this.view.hasAccessToAll);
        this.hasAccessToAll.setCaption('Has Access to All Modifiers?');
        this.hasAccessToAll.setItems(
            new SelectOption(true, 'Yes'),
            new SelectOption(false, 'No')
        );
        this.hasAccessToAll.valueChanged.register(this.onHasAccessToAllChanged.bind(this));
        this.userModifiers = new ListGroup(this.view.userModifiers);
        this.backCommand.add(this.view.backButton);
    }

    private onHasAccessToAllChanged(hasAccessToAll: boolean) {
        if (hasAccessToAll) {
            this.view.hideUserModifiers();
        }
        else {
            this.view.showUserModifiers();
        }
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
        this.userModifiers.setItems(
            sourceItems,
            (sourceItem: IAppRoleModel, view: EditUserModifierListItemView) => {
                let listItem = new EditUserModifierListItem(this.hubApi, view);
                listItem.setUserID(this.userID);
                listItem.withAssignedModifier(sourceItem);
                return listItem;
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
        this.awaitable.resolve(
            UserModCategoryPanelResult.backRequested
        );
    }
}