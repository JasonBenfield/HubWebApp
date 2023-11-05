import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { DelayedAction } from '@jasonbenfield/sharedwebapp/DelayedAction';
import { EditCurrentUserForm } from "../../Lib/Http/EditCurrentUserForm";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { UserEditPanelView } from "./UserEditPanelView";

interface IResult {
    canceled?: boolean;
    saved?: { user: IAppUserModel };
}

class Result {
    static canceled() { return new Result({ canceled: true }); }

    static saved(user: IAppUserModel) {
        return new Result({ saved: { user: user } });
    }

    private constructor(private readonly results: IResult) {}

    get canceled() { return this.results.canceled; }

    get saved() { return this.results.saved; }
}

export class UserEditPanel implements IPanel {
    private readonly alert: MessageAlert;
    private readonly editUserForm: EditCurrentUserForm;
    private readonly awaitable = new Awaitable<Result>();
    private readonly cancelCommand = new Command(this.cancel.bind(this));
    private readonly saveCommand = new AsyncCommand(this.save.bind(this));

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: UserEditPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
        this.cancelCommand.add(this.view.cancelButton);
        this.saveCommand.add(this.view.saveButton);
        new TextComponent(this.view.titleHeader).setText('Edit User');
        this.editUserForm = new EditCurrentUserForm(this.view.editUserForm);
        this.editUserForm.handleSubmit(this.onFormSubmit.bind(this));
    }

    private onFormSubmit(el: HTMLElement, evt: JQuery.Event) {
        evt.preventDefault();
        this.saveCommand.execute();
    }

    setUser(user: IAppUserModel) {
        this.editUserForm.PersonName.setValue(user.Name.DisplayText);
        this.editUserForm.Email.setValue(user.Email);
    }

    start() {
        return this.awaitable.start();
    }

    private cancel() {
        this.awaitable.resolve(Result.canceled());
    }

    private async save() {
        const result = await this.alert.infoAction(
            'Saving...',
            () => this.editUserForm.save(this.hubClient.CurrentUser.EditUserAction),
        );
        if (result.succeeded()) {
            this.awaitable.resolve(Result.saved(result.value));
        }
    }

    activate() {
        this.view.show();
        new DelayedAction(
            () => this.editUserForm.PersonName.setFocus(),
            1000
        ).execute();
    }

    deactivate() { this.view.hide(); }
}