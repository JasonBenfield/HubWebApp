import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { AsyncCommand } from "@jasonbenfield/sharedwebapp/Command/AsyncCommand";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { EditUserForm } from '../../../Hub/Api/EditUserForm';
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserEditPanelView } from "./UserEditPanelView";

interface Results {
    canceled?: {};
    saved?: {};
}

export class UserEditPanelResult {
    static get canceled() { return new UserEditPanelResult({ canceled: {} }); }

    static get saved() { return new UserEditPanelResult({ saved: {} }); }

    private constructor(private readonly results: Results) {
    }

    get canceled() { return this.results.canceled; }

    get saved() { return this.results.saved; }
}

export class UserEditPanel implements IPanel {
    private readonly alert: MessageAlert;
    private readonly editUserForm: EditUserForm;
    private userID: number;
    private readonly awaitable = new Awaitable<UserEditPanelResult>();
    private readonly cancelCommand = new Command(this.cancel.bind(this));
    private readonly saveCommand = new AsyncCommand(this.save.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserEditPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.cancelCommand.add(this.view.cancelButton);
        this.saveCommand.add(this.view.saveButton);
        new CardTitleHeader('Edit User', this.view.titleHeader);
        this.editUserForm = new EditUserForm(this.view.editUserForm);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let userForm = await this.getUserForEdit(this.userID);
        this.editUserForm.import(userForm);
        await new DelayedAction(
            () => this.editUserForm.PersonName.setFocus(),
            1000
        ).execute();
    }

    private async getUserForEdit(userID: number) {
        let userForm: Record<string, object>;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                userForm = await this.hubApi.UserMaintenance.GetUserForEdit(userID);
            }
        );
        return userForm;
    }

    start() {
        return this.awaitable.start();
    }

    private cancel() {
        this.awaitable.resolve(UserEditPanelResult.canceled);
    }

    private async save() {
        let result = await this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction);
        if (result.succeeded()) {
            this.awaitable.resolve(UserEditPanelResult.saved);
        }
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}