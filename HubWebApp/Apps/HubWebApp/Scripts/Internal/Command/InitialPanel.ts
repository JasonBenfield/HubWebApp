import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { AppCommand } from "../../Lib/AppCommand";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { InitialPanelView } from "./InitialPanelView";

interface IResult {
    commandLoaded?: { command: AppCommand; };
}

class Result {
    static commandLoaded(command: AppCommand) {
        return new Result({ commandLoaded: { command: command } });
    }

    private constructor(private readonly result: IResult) { }

    get commandLoaded() { return this.result.commandLoaded; }
}

export class InitialPanel implements IPanel {
    private readonly awaitable: Awaitable<Result>;
    private readonly alert: MessageAlert;
    private commandID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: InitialPanelView) {
        this.alert = new MessageAlert(view.alertView);
    }

    setCommandID(commandID: number) {
        this.commandID = commandID;
    }

    start() {
        return this.awaitable.start();
    }

    async refresh() {
        const sourceCommand = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.Command.
        )
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}