import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { AppUserOptions } from "./AppUserOptions";
import { AppUserDataPanelView } from "./AppUserDataPanelView";
import { App } from "../../Lib/App";
import { AppUser } from "../../Lib/AppUser";
import { Modifier } from "../../Lib/Modifier";

interface IResult {
    done?: { appUserOptions: AppUserOptions; };
}

class Result {
    static done(appUserData: AppUserOptions) {
        return new Result(
            { done: { appUserOptions: appUserData } }
        );
    }

    private constructor(private readonly results: IResult) { }

    get done() { return this.results.done; }
}

export class AppUserDataPanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private userID: number;

    constructor(
        private readonly hubClient: HubAppClient,
        private readonly view: AppUserDataPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    start() {
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        const appUserData = await this.alert.infoAction(
            'Loading...',
            async () => {
                const sourceApp = await this.hubClient.App.GetApp();
                const app = new App(sourceApp);
                const sourceUser = await this.hubClient.UserInquiry.GetUser(this.userID);
                const user = new AppUser(sourceUser);
                const sourceDefaultModifier = await this.hubClient.App.GetDefaultModifier();
                const defaultModifier = new Modifier(sourceDefaultModifier);
                return new AppUserOptions(app, user, defaultModifier);
            }
        );
        return this.awaitable.resolve(Result.done(appUserData));
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}