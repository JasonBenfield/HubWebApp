﻿import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Card/CardAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { ListGroup } from "@jasonbenfield/sharedwebapp/ListGroup/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AddRolePanelView } from "./AddRolePanelView";
import { AppUserOptions } from "./AppUserOptions";
import { RoleButtonListItemView } from "./RoleButtonListItemView";
import { RoleListItem } from "./RoleListItem";

interface Results {
    back?: {};
    roleSelected?: {};
}

export class AddRolePanelResult {
    static back() { return new AddRolePanelResult({ back: {} }); }

    static roleSelected() { return new AddRolePanelResult({ roleSelected: {} }); }

    private constructor(private readonly results: Results) {
    }

    get back() { return this.results.back; }

    get roleSelected() { return this.results.roleSelected; }
}

export class AddRolePanel implements IPanel {
    private readonly awaitable: Awaitable<AddRolePanelResult>;
    private readonly alert: MessageAlert;
    private readonly roles: ListGroup;
    private user: IAppUserModel;
    private modifier: IModifierModel;
    private defaultModifier: IModifierModel;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AddRolePanelView
    ) {
        new TextBlock('Select Role', view.titleHeader);
        this.awaitable = new Awaitable();
        this.alert = new CardAlert(view.alert).alert;
        this.roles = new ListGroup(view.roles);
        this.roles.itemClicked.register(this.onRoleClicked.bind(this));
    }

    private async onRoleClicked(roleListItem: RoleListItem) {
        await this.addRole(roleListItem.role);
        this.awaitable.resolve(AddRolePanelResult.roleSelected());
    }

    setAppUserOptions(appUserOptions: AppUserOptions) {
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    }

    setDefaultModifier() {
        this.setModifier(this.defaultModifier);
    }

    setModifier(modifier: IModifierModel) {
        this.modifier = modifier;
    }

    private addRole(role: IAppRoleModel) {
        return this.alert.infoAction(
            'Adding role...',
            () => this.hubApi.AppUserMaintenance.AssignRole({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID,
                RoleID: role.ID
            })
        );
    }

    start() {
        this.roles.clearItems();
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let roles = await this.alert.infoAction(
            'Loading...',
            () => this.hubApi.AppUser.GetUnassignedRoles({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID
            })
        );
        this.roles.setItems(
            roles,
            (role, view: RoleButtonListItemView) => new RoleListItem(role, view)
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}