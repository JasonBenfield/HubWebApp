import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Components/Command";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { EventCollection } from "@jasonbenfield/sharedwebapp/Events";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppUserOptions } from "./AppUserOptions";
import { UserRoleListItem } from "./UserRoleListItem";
import { UserRoleListItemView } from "./UserRoleListItemView";
import { UserRolesPanelView } from "./UserRolesPanelView";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { RoleListItem } from "./RoleListItem";

interface Results {
    addRequested?: {};
    modifierRequested?: {};
}

class Result {
    static addRequested() { return new Result({ addRequested: {} }); }

    static modifierRequested() {
        return new Result({ modifierRequested: {} });
    }

    private constructor(private readonly results: Results) { }

    get addRequested() { return this.results.addRequested; }

    get modifierRequested() { return this.results.modifierRequested; }
}

export class UserRolesPanel implements IPanel {
    private readonly appName: TextComponent;
    private readonly appType: TextComponent;
    private readonly userName: TextComponent;
    private readonly personName: TextComponent;
    private readonly categoryName: TextComponent;
    private readonly modifierDisplayText: TextComponent;
    private readonly alert: MessageAlert;
    private readonly userRoles: ListGroup;
    private readonly awaitable: Awaitable<Result>;
    private user: IAppUserModel;
    private defaultModifier: IModifierModel;
    private modifier: IModifierModel;
    private readonly addCommand: Command;
    private readonly allowAccessCommand: AsyncCommand;
    private readonly denyAccessCommand: AsyncCommand;
    private readonly defaultUserRoles: ListGroup;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserRolesPanelView
    ) {
        this.appName = new TextComponent(view.appName);
        this.appType = new TextComponent(view.appType);
        this.userName = new TextComponent(view.userName);
        this.personName = new TextComponent(view.personName);
        this.categoryName = new TextComponent(view.categoryName);
        this.modifierDisplayText = new TextComponent(view.modifierDisplayText);
        this.alert = new CardAlert(view.alert).alert;
        this.userRoles = new ListGroup(view.userRoles);
        view.handleUserRoleDeleteClicked(this.onDeleteRoleClicked.bind(this));
        this.awaitable = new Awaitable();
        this.addCommand = new Command(this.requestAdd.bind(this));
        this.addCommand.add(view.addButton);
        new Command(this.requestModifier.bind(this)).add(view.selectModifierButton);
        this.allowAccessCommand = new AsyncCommand(this.allowAccess.bind(this));
        this.allowAccessCommand.add(view.allowAccessButton);
        this.denyAccessCommand = new AsyncCommand(this.denyAccess.bind(this));
        this.denyAccessCommand.add(view.denyAccessButton);
        new TextComponent(view.defaultUserRolesTitle).setText('Default Roles');
        this.defaultUserRoles = new ListGroup(view.defaultUserRoles);
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() {
        this.hubApi.Users.Index.open({ UserID: this.user.ID });
    }

    private requestAdd() {
        this.awaitable.resolve(Result.addRequested());
    }

    private requestModifier() {
        this.awaitable.resolve(Result.modifierRequested());
    }

    private async allowAccess() {
        await this.alert.infoAction(
            'Allowing Access...',
            () => this.hubApi.AppUserMaintenance.AllowAccess({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID
            })
        );
        await this.refresh();
    }

    private async denyAccess() {
        await this.alert.infoAction(
            'Denying Access...',
            () => this.hubApi.AppUserMaintenance.DenyAccess({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID
            })
        );
        await this.refresh();
    }

    setAppUserOptions(appUserOptions: AppUserOptions) {
        this.appName.setText(appUserOptions.app.AppKey.Name.DisplayText);
        this.appType.setText(appUserOptions.app.AppKey.Type.DisplayText);
        this.userName.setText(appUserOptions.user.UserName.DisplayText);
        this.personName.setText(appUserOptions.user.Name.DisplayText);
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    }

    setDefaultModifier() {
        this.categoryName.setText('Default');
        this.modifierDisplayText.setText('');
        this.modifier = this.defaultModifier;
    }

    setModCategory(modCategory: IModifierCategoryModel) {
        this.categoryName.setText(modCategory.Name.DisplayText);
    }

    setModifier(modifier: IModifierModel) {
        this.modifierDisplayText.setText(modifier.DisplayText);
        this.modifier = modifier;
    }

    start() {
        new DelayedAction(this.refresh.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async refresh() {
        this.alert.clear();
        this.allowAccessCommand.hide();
        this.denyAccessCommand.hide();
        this.addCommand.hide();
        let isDefaultModifier = this.modifier.ID === this.defaultModifier.ID;
        let userAccess: IUserAccessModel;
        let defaultUserAccess: IUserAccessModel;
        await this.alert.infoAction(
            'Loading',
            async () => {
                userAccess = await this.hubApi.AppUser.GetUserAccess({
                    UserID: this.user.ID,
                    ModifierID: this.modifier.ID
                });
                if (isDefaultModifier) {
                    defaultUserAccess = userAccess;
                }
                else {
                    defaultUserAccess = await this.hubApi.AppUser.GetUserAccess({
                        UserID: this.user.ID,
                        ModifierID: this.defaultModifier.ID
                    });
                }
            }
        );
        if (userAccess.HasAccess) {
            this.denyAccessCommand.show();
            this.addCommand.show();
        }
        else {
            this.allowAccessCommand.show();
        }
        this.userRoles.setItems(
            userAccess.AssignedRoles,
            (role: IAppRoleModel, itemView: UserRoleListItemView) =>
                new UserRoleListItem(role, itemView)
        );
        if (isDefaultModifier) {
            this.view.hideDefaultUserRoles();
        }
        else {
            if (defaultUserAccess.AssignedRoles.length > 0) {
                this.view.showDefaultUserRoles();
            }
            else {
                this.view.hideDefaultUserRoles();
            }
            this.defaultUserRoles.setItems(
                defaultUserAccess.AssignedRoles,
                (role: IAppRoleModel, itemView: UserRoleListItemView) => {
                    const listItem = new UserRoleListItem(role, itemView);
                    listItem.hideDeleteButton();
                    return listItem;
                }
            );
        }
        if (!userAccess.HasAccess) {
            this.alert.danger('Access is denied.');
        }
        else if (userAccess.AssignedRoles.length === 0) {
            this.alert.warning('No roles have been assigned.');
        }
    }

    private async onDeleteRoleClicked(el: HTMLElement) {
        const roleListItem = this.userRoles.getItemByElement(el) as UserRoleListItem;
        await this.alert.infoAction(
            'Removing role...',
            () => this.hubApi.AppUserMaintenance.UnassignRole({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID,
                RoleID: roleListItem.role.ID
            })
        );
        await this.refresh();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}