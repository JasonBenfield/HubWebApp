import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { DelayedAction } from "@jasonbenfield/sharedwebapp/DelayedAction";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../Hub/Api/HubAppApi";
import { AppUserOptions } from "./AppUserOptions";
import { AppUserDataPanelView } from "./AppUserDataPanelView";

interface Results {
    done?: { appUserOptions: AppUserOptions; };
}

export class AppUserDataPanelResult {
    static done(appUserData: AppUserOptions) {
        return new AppUserDataPanelResult(
            { done: { appUserOptions: appUserData } }
        );
    }

    private constructor(private readonly results: Results) { }

    get done() { return this.results.done; }
}

export class AppUserDataPanel implements IPanel {
    private readonly awaitable = new Awaitable<AppUserDataPanelResult>();
    private readonly alert: MessageAlert;
    private userID: number;

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppUserDataPanelView
    ) {
        this.alert = new MessageAlert(this.view.alert);
    }

    start(userID: number) {
        this.userID = userID;
        new DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    }

    private async delayedStart() {
        let appUserData: AppUserOptions;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                let app = await this.hubApi.App.GetApp();
                let user = await this.hubApi.UserInquiry.GetUser(this.userID);
                let defaultModifier = await this.hubApi.App.GetDefaultModiifer();
                appUserData = new AppUserOptions(app, user, defaultModifier);
            }
        );
        return this.awaitable.resolve(AppUserDataPanelResult.done(appUserData));
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}