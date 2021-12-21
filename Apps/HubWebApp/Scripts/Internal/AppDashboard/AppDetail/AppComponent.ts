import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { CardView } from '@jasonbenfield/sharedwebapp/Card/CardView';
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { AppComponentView } from "./AppComponentView";

export class AppComponent extends CardView {
    private readonly alert: MessageAlert;

    constructor(private readonly hubApi: HubAppApi, private readonly view: AppComponentView) {
        super();
        new CardTitleHeader('App', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    async refresh() {
        let app = await this.getApp();
        this.view.setAppName(app.AppName);
        this.view.setAppTitle(app.Title);
        this.view.setAppType(app.Type.DisplayText);
    }

    private async getApp() {
        let app: IAppModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                app = await this.hubApi.App.GetApp();
            }
        );
        return app;
    }
}