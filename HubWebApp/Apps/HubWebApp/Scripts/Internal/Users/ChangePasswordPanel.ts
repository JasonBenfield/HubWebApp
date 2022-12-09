import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { ChangePasswordPanelView } from "./ChangePasswordPanelView";
import { ChangePasswordForm } from '../../Lib/Api/ChangePasswordForm';
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";

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
    private readonly changePasswordForm: ChangePasswordForm;
    private readonly saveCommand; AsyncCommand;
    private userID: number;

    constructor(private readonly hubApi: HubAppApi, private readonly view: ChangePasswordPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.changePasswordForm = new ChangePasswordForm(view.changePasswordForm);
        this.changePasswordForm.handleSubmit(this.onFormSubmit.bind(this));
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
    }

    private onFormSubmit(el: HTMLElement, evt: JQueryEventObject) {
        evt.preventDefault();
        this.saveCommand.execute();
    }

    private cancel() {
        this.awaitable.resolve(Result.done());
    }

    private async save() {
        const result = await this.alert.infoAction(
            'Saving...',
            () => this.changePasswordForm.save(this.hubApi.UserMaintenance.ChangePasswordAction)
        );
        if (result.succeeded()) {
            this.awaitable.resolve(Result.done());
        }
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    start() {
        return this.awaitable.start();
    }

    activate() {
        this.view.show();
        this.changePasswordForm.UserID.setValue(this.userID);
        this.changePasswordForm.Password.setValue('');
        this.changePasswordForm.Confirm.setValue('');
        new DelayedAction(
            () => this.changePasswordForm.Password.setFocus(),
            1000
        ).execute();
    }

    deactivate() { this.view.hide(); }

}