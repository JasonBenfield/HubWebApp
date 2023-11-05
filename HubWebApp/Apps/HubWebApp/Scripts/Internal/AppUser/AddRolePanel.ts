import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AddRolePanelView } from "./AddRolePanelView";
import { AppUserOptions } from "./AppUserOptions";
import { RoleButtonListItemView } from "./RoleButtonListItemView";
import { RoleListItem } from "./RoleListItem";
import { Command } from "@jasonbenfield/sharedwebapp/Components/Command";

interface IResult {
    back?: boolean;
    roleSelected?: boolean;
}

class Result {
    static back() { return new Result({ back: true }); }

    static roleSelected() { return new Result({ roleSelected: true }); }

    private constructor(private readonly results: IResult) {
    }

    get back() { return this.results.back; }

    get roleSelected() { return this.results.roleSelected; }
}

export class AddRolePanel implements IPanel {
    private readonly awaitable: Awaitable<Result>;
    private readonly alert: MessageAlert;
    private readonly roles: ListGroup<RoleListItem, RoleButtonListItemView>;
    private user: IAppUserModel;
    private modifier: IModifierModel;
    private defaultModifier: IModifierModel;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AddRolePanelView
    ) {
        new TextComponent(view.titleHeader).setText('Select Role');
        this.awaitable = new Awaitable();
        this.alert = new CardAlert(view.alert).alert;
        this.roles = new ListGroup(view.roles);
        this.roles.registerItemClicked(this.onRoleClicked.bind(this));
        new Command(this.back.bind(this)).add(view.backButton);
    }

    private back() { this.awaitable.resolve(Result.back()); }

    private async onRoleClicked(roleListItem: RoleListItem) {
        await this.addRole(roleListItem.role);
        this.awaitable.resolve(Result.roleSelected());
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
            () => this.hubClient.AppUserMaintenance.AssignRole({
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
        const roles = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.AppUser.GetUnassignedRoles({
                UserID: this.user.ID,
                ModifierID: this.modifier.ID
            })
        );
        this.roles.setItems(
            roles,
            (role, view) => new RoleListItem(role, view)
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}