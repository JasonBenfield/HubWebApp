import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { EditUserForm } from '../../../Lib/Api/EditUserForm';
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { UserEditPanelView } from "./UserEditPanelView";

interface IResult {
    canceled?: {};
    saved?: {};
}

class Result {
    static canceled() { return new Result({ canceled: {} }); }

    static saved() { return new Result({ saved: {} }); }

    private constructor(private readonly results: IResult) {
    }

    get canceled() { return this.results.canceled; }

    get saved() { return this.results.saved; }
}

export class UserEditPanel implements IPanel {
    private readonly alert: MessageAlert;
    private readonly editUserForm: EditUserForm;
    private userID: number;
    private readonly awaitable = new Awaitable<Result>();
    private readonly cancelCommand = new Command(this.cancel.bind(this));
    private readonly saveCommand = new AsyncCommand(this.save.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserEditPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.cancelCommand.add(this.view.cancelButton);
        this.saveCommand.add(this.view.saveButton);
        new TextComponent(this.view.titleHeader).setText('Edit User');
        this.editUserForm = new EditUserForm(this.view.editUserForm);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        const userForm = await this.getUserForEdit(this.userID);
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
        this.awaitable.resolve(Result.canceled());
    }

    private async save() {
        const result = await this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction);
        if (result.succeeded()) {
            this.awaitable.resolve(Result.saved());
        }
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}