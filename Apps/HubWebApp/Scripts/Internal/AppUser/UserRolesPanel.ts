import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { EventCollection } from "@jasonbenfield/sharedwebapp/Events";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppUserOptions } from "./AppUserOptions";
import { UserRoleListItem } from "./UserRoleListItem";
import { UserRoleListItemView } from "./UserRoleListItemView";
import { UserRolesPanelView } from "./UserRolesPanelView";

interface Results {
    addRequested?: {};
    modifierRequested?: {};
}

export class UserRolesPanelResult {
    static addRequested() { return new UserRolesPanelResult({ addRequested: {} }); }

    static modifierRequested() {
        return new UserRolesPanelResult({ modifierRequested: {} });
    }

    private constructor(private readonly results: Results) { }

    get addRequested() { return this.results.addRequested; }

    get modifierRequested() { return this.results.modifierRequested; }
}

export class UserRolesPanel implements IPanel {
    private readonly appName: TextBlock;
    private readonly appType: TextBlock;
    private readonly userName: TextBlock;
    private readonly personName: TextBlock;
    private readonly categoryName: TextBlock;
    private readonly modifierDisplayText: TextBlock;
    private readonly alert: MessageAlert;
    private readonly userRoles: ListGroup;
    private readonly userRoleListItems: UserRoleListItem[] = [];
    private readonly awaitable: Awaitable<UserRolesPanelResult>;
    private user: IAppUserModel;
    private defaultModifier: IModifierModel;
    private modifier: IModifierModel;
    private readonly deleteEvents = new EventCollection();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserRolesPanelView
    ) {
        this.appName = new TextBlock('', view.appName);
        this.appType = new TextBlock('', view.appType);
        this.userName = new TextBlock('', view.userName);
        this.personName = new TextBlock('', view.personName);
        this.categoryName = new TextBlock('', view.categoryName);
        this.modifierDisplayText = new TextBlock('', view.modifierDisplayText);
        this.alert = new MessageAlert(view.alert);
        this.userRoles = new ListGroup(view.userRoles);
        this.awaitable = new Awaitable();
        new Command(this.requestAdd.bind(this)).add(view.addButton);
        new Command(this.requestModifier.bind(this)).add(view.selectModifierButton);
    }

    private requestAdd() {
        this.awaitable.resolve(UserRolesPanelResult.addRequested());
    }

    private requestModifier() {
        this.awaitable.resolve(UserRolesPanelResult.modifierRequested());
    }

    setAppUserOptions(appUserOptions: AppUserOptions) {
        this.appName.setText(appUserOptions.app.AppName);
        this.appType.setText(appUserOptions.app.Type.DisplayText);
        this.userName.setText(appUserOptions.user.UserName);
        this.personName.setText(appUserOptions.user.Name);
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    }

    setDefaultModifier() {
        this.categoryName.setText('Default');
        this.modifierDisplayText.setText('');
        this.modifier = this.defaultModifier;
    }

    setModCategory(modCategory: IModifierCategoryModel) {
        this.categoryName.setText(modCategory.Name);
    }

    setModifier(modifier: IModifierModel) {
        this.modifierDisplayText.setText(modifier.DisplayText);
        this.modifier = modifier;
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let userRoles = await this.alert.infoAction(
            'Loading',
            () => this.hubApi.AppUser.GetUserRoles({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID
            })
        );
        this.deleteEvents.unregisterAll();
        for (let listItem of this.userRoleListItems) {
            listItem.dispose();
        }
        let userRoleListItems = this.userRoles.setItems(
            userRoles,
            (role: IAppRoleModel, itemView: UserRoleListItemView) =>
                new UserRoleListItem(role, itemView)
        );
        for (let listItem of userRoleListItems) {
            this.deleteEvents.register(
                listItem.deleteButtonClicked,
                this.onDeleteRoleClicked.bind(this)
            );
        }
        this.userRoleListItems.splice(0, this.userRoleListItems.length, ...userRoleListItems);
    }

    private onDeleteRoleClicked(role: IAppRoleModel) {
        return this.alert.infoAction(
            'Removing role...',
            () => this.hubApi.AppUserMaintenance.UnassignRole({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID,
                RoleID: role.ID
            })
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}