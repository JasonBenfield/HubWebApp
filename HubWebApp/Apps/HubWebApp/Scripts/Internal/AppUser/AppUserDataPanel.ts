import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../Lib/Api/HubAppApi";
import { AppUserOptions } from "./AppUserOptions";
import { AppUserDataPanelView } from "./AppUserDataPanelView";

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
        private readonly hubApi: HubAppApi,
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
        let appUserData: AppUserOptions;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                const app = await this.hubApi.App.GetApp();
                const user = await this.hubApi.UserInquiry.GetUser(this.userID);
                const defaultModifier = await this.hubApi.App.GetDefaultModifier();
                appUserData = new AppUserOptions(app, user, defaultModifier);
            }
        );
        return this.awaitable.resolve(Result.done(appUserData));
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}