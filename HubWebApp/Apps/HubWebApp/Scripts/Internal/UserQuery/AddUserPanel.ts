import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { AddUserForm } from "../../Lib/Api/AddUserForm";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AddUserPanelView } from "./AddUserPanelView";

interface IResult {
    done?: { saved: boolean; };
}

class Result {
    static done(saved: boolean) { return new Result({ done: { saved: saved } }); }

    private constructor(private readonly result: IResult) { }

    get done() { return this.result.done; }
}


export class AddUserPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly addUserForm: AddUserForm;
    private readonly saveCommand: AsyncCommand;

    constructor(private readonly hubApi: HubAppApi, private readonly view: AddUserPanelView) {
        this.addUserForm = new AddUserForm(view.addUserForm);
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
        view.addUserForm.handleSubmit(this.onFormSubmit.bind(this));
    }

    private onFormSubmit(el: HTMLElement, evt: JQueryEventObject) {
        evt.preventDefault();
        this.saveCommand.execute();
    }

    private cancel() { this.awaitable.resolve(Result.done(false)); }

    private async save() {
        const result = await this.addUserForm.save(this.hubApi.Users.AddUserAction);
        if (result.succeeded()) {
            this.awaitable.resolve(Result.done(true));
        }
    }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
        this.view.setFocus();
    }

    deactivate() { this.view.hide(); }

}