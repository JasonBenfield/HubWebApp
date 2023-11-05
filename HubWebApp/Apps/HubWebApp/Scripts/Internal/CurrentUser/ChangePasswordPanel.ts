import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { ChangeCurrentUserPasswordForm } from "../../Lib/Http/ChangeCurrentUserPasswordForm";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { ChangePasswordPanelView } from "./ChangePasswordPanelView";

interface IResult {
    done?: boolean;
}

class Result {
    static done() { return new Result({ done: true }); }

    private constructor(private readonly result: IResult) { }

    get done() { return this.result.done; }
}

export class ChangePasswordPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly changePasswordForm: ChangeCurrentUserPasswordForm;
    private readonly saveCommand; AsyncCommand;

    constructor(private readonly hubClient: HubAppClient, private readonly view: ChangePasswordPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.changePasswordForm = new ChangeCurrentUserPasswordForm(view.changePasswordForm);
        this.changePasswordForm.handleSubmit(this.onFormSubmit.bind(this));
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
    }

    private onFormSubmit(el: HTMLElement, evt: JQuery.Event) {
        evt.preventDefault();
        this.saveCommand.execute();
    }

    private cancel() {
        this.awaitable.resolve(Result.done());
    }

    private async save() {
        const result = await this.alert.infoAction(
            'Saving...',
            () => this.changePasswordForm.save(this.hubClient.CurrentUser.ChangePasswordAction)
        );
        if (result.succeeded()) {
            this.awaitable.resolve(Result.done());
        }
    }

    start() {
        return this.awaitable.start();
    }

    activate() {
        this.view.show();
        this.changePasswordForm.Password.setValue('');
        this.changePasswordForm.Confirm.setValue('');
        new DelayedAction(
            () => this.changePasswordForm.Password.setFocus(),
            1000
        ).execute();
    }

    deactivate() { this.view.hide(); }

}