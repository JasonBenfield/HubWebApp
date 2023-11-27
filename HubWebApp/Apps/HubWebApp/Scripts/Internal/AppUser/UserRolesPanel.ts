import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppUserOptions } from "./AppUserOptions";
import { UserRoleListItem } from "./UserRoleListItem";
import { UserRoleListItemView } from "./UserRoleListItemView";
import { UserRolesPanelView } from "./UserRolesPanelView";
import { Modifier } from "../../Lib/Modifier";
import { ModifierCategory } from "../../Lib/ModifierCategory";
import { AppUser } from "../../Lib/AppUser";
import { UserAccess } from "../../Lib/UserAccess";

interface Results {
    addRequested?: boolean;
    modifierRequested?: boolean;
}

class Result {
    static addRequested() { return new Result({ addRequested: true }); }

    static modifierRequested() {
        return new Result({ modifierRequested: true });
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
    private readonly userRoles: ListGroup<UserRoleListItem, UserRoleListItemView>;
    private readonly awaitable: Awaitable<Result>;
    private user: AppUser;
    private defaultModifier: Modifier;
    private modifier: Modifier;
    private readonly addCommand: Command;
    private readonly allowAccessCommand: AsyncCommand;
    private readonly denyAccessCommand: AsyncCommand;
    private readonly defaultUserRoles: ListGroup<UserRoleListItem, UserRoleListItemView>;

    constructor(
        private readonly hubClient: HubAppClient,
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
        this.hubClient.Users.Index.open({ UserID: this.user.id, ReturnTo: '' });
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
            () => this.hubClient.AppUserMaintenance.AllowAccess({
                UserID: this.user.id,
                ModifierID: this.modifier.id
            })
        );
        await this.refresh();
    }

    private async denyAccess() {
        await this.alert.infoAction(
            'Denying Access...',
            () => this.hubClient.AppUserMaintenance.DenyAccess({
                UserID: this.user.id,
                ModifierID: this.modifier.id
            })
        );
        await this.refresh();
    }

    setAppUserOptions(appUserOptions: AppUserOptions) {
        this.appName.setText(appUserOptions.app.appKey.name.displayText);
        this.appType.setText(appUserOptions.app.appKey.type.DisplayText);
        this.userName.setText(appUserOptions.user.userName.displayText);
        this.personName.setText(appUserOptions.user.name.displayText);
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    }

    setDefaultModifier() {
        this.categoryName.setText('Default');
        this.modifierDisplayText.setText('');
        this.modifier = this.defaultModifier;
    }

    setModCategory(modCategory: ModifierCategory) {
        this.categoryName.setText(modCategory.name.displayText);
    }

    setModifier(modifier: Modifier) {
        this.modifierDisplayText.setText(modifier.displayText);
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
        let isDefaultModifier = this.modifier.id === this.defaultModifier.id;
        let userAccess: UserAccess;
        let defaultUserAccess: UserAccess;
        await this.alert.infoAction(
            'Loading',
            async () => {
                const sourceUserAccess = await this.hubClient.AppUser.GetUserAccess({
                    UserID: this.user.id,
                    ModifierID: this.modifier.id
                });
                userAccess = new UserAccess(sourceUserAccess);
                if (isDefaultModifier) {
                    defaultUserAccess = userAccess;
                }
                else {
                    const sourceDefaultUserAccess = await this.hubClient.AppUser.GetUserAccess({
                        UserID: this.user.id,
                        ModifierID: this.defaultModifier.id
                    });
                    defaultUserAccess = new UserAccess(sourceDefaultUserAccess);
                }
            }
        );
        if (userAccess.hasAccess) {
            this.denyAccessCommand.show();
            this.addCommand.show();
        }
        else {
            this.allowAccessCommand.show();
        }
        this.userRoles.setItems(
            userAccess.assignedRoles,
            (role, itemView) =>
                new UserRoleListItem(role, itemView)
        );
        if (isDefaultModifier) {
            this.view.hideDefaultUserRoles();
        }
        else {
            if (defaultUserAccess.assignedRoles.length > 0) {
                this.view.showDefaultUserRoles();
            }
            else {
                this.view.hideDefaultUserRoles();
            }
            this.defaultUserRoles.setItems(
                defaultUserAccess.assignedRoles,
                (role, itemView) => {
                    const listItem = new UserRoleListItem(role, itemView);
                    listItem.hideDeleteButton();
                    return listItem;
                }
            );
        }
        if (!userAccess.hasAccess) {
            this.alert.danger('Access is denied.');
        }
        else if (userAccess.assignedRoles.length === 0) {
            this.alert.warning('No roles have been assigned.');
        }
    }

    private async onDeleteRoleClicked(el: HTMLElement) {
        const roleListItem = this.userRoles.getItemByElement(el);
        await this.alert.infoAction(
            'Removing role...',
            () => this.hubClient.AppUserMaintenance.UnassignRole({
                UserID: this.user.id,
                ModifierID: this.modifier.id,
                RoleID: roleListItem.role.id
            })
        );
        await this.refresh();
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}