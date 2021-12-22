import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { StartPanelView } from "./StartPanelView";

interface Results {
    done?: {};
}

export class StartPanelResult {
    static done() { return new StartPanelResult({ done: {} }); }

    private constructor(private readonly results: Results) { }

    get done() { return this.results.done; }
}

export class StartPanel {
    private readonly awaitable = new Awaitable<StartPanelResult>();
    private readonly alert: MessageAlert;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: StartPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        return 0;
    }

    private async getApp() {
        let app: IAppModel;
        //await this.alert.infoAction(
        //    async 
        //);
        return app;
    }
}