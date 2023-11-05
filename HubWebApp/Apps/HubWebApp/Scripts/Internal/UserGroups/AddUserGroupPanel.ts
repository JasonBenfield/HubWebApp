import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { InputControl } from "@jasonbenfield/sharedwebapp/Components/InputControl";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextToTextViewValue } from "@jasonbenfield/sharedwebapp/Forms/TextToTextViewValue";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AddUserGroupPanelView } from "./AddUserGroupPanelView";

interface IResult {
    done?: { saved: boolean };
}

class Result {
    static done(saved: boolean) { return new Result({ done: { saved } }); }

    private constructor(private readonly result: IResult) { }

    get done() { return this.result.done; }
}

export class AddUserGroupPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly groupName: InputControl<string>;
    private readonly saveCommand: AsyncCommand;

    constructor(private readonly hubApi: HubAppClient, private readonly view: AddUserGroupPanelView) {
        this.alert = new MessageAlert(view.alert);
        this.groupName = new InputControl<string>(view.groupNameInput, new TextToTextViewValue());
        new Command(this.cancel.bind(this)).add(view.cancelButton);
        this.saveCommand = new AsyncCommand(this.save.bind(this));
        this.saveCommand.add(view.saveButton);
        view.handleFormSubmitted(this.onFormSubmitted.bind(this));
    }

    private onFormSubmitted(el: HTMLElement, evt: JQueryEventObject) {
        this.saveCommand.execute();
        evt.preventDefault();
    }

    private cancel() {
        this.awaitable.resolve(Result.done(false));
    }

    private async save() {
        await this.alert.infoAction(
            'Saving',
            () => this.hubApi.UserGroups.AddUserGroupIfNotExists({
                GroupName: this.groupName.getValue()
            })
        );
        this.awaitable.resolve(Result.done(true));
    }

    start() { return this.awaitable.start(); }

    activate() {
        this.view.show();
        this.groupName.setValue('');
        this.groupName.setFocus();
    }

    deactivate() { this.view.hide(); }
}