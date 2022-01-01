﻿import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Command/AsyncCommand";
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
    private readonly addCommand: Command;
    private readonly allowAccessCommand: AsyncCommand;
    private readonly denyAccessCommand: AsyncCommand;
    private readonly defaultUserRoles: ListGroup;

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
        this.alert = new CardAlert(view.alert).alert;
        this.userRoles = new ListGroup(view.userRoles);
        this.awaitable = new Awaitable();
        this.addCommand = new Command(this.requestAdd.bind(this));
        this.addCommand.add(view.addButton);
        new Command(this.requestModifier.bind(this)).add(view.selectModifierButton);
        this.allowAccessCommand = new AsyncCommand(this.allowAccess.bind(this));
        this.allowAccessCommand.add(view.allowAccessButton);
        this.denyAccessCommand = new AsyncCommand(this.denyAccess.bind(this));
        this.denyAccessCommand.add(view.denyAccessButton);
        new TextBlock('Default Roles', view.defaultUserRolesTitle);
        this.defaultUserRoles = new ListGroup(view.defaultUserRoles);
    }

    private requestAdd() {
        this.awaitable.resolve(UserRolesPanelResult.addRequested());
    }

    private requestModifier() {
        this.awaitable.resolve(UserRolesPanelResult.modifierRequested());
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
        this.deleteEvents.unregisterAll();
        for (let listItem of this.userRoleListItems) {
            listItem.dispose();
        }
        let userRoleListItems = this.userRoles.setItems(
            userAccess.AssignedRoles,
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
        if (isDefaultModifier) {
            this.view.hideDefaultUserRoles();
        }
        else {
            this.view.showDefaultUserRoles();
            this.defaultUserRoles.setItems(
                defaultUserAccess.AssignedRoles,
                (role: IAppRoleModel, itemView: UserRoleListItemView) => {
                    let listItem = new UserRoleListItem(role, itemView);
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