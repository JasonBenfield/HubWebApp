import { CardView } from '@jasonbenfield/sharedwebapp/Card/CardView';
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { AppComponentView } from "./AppComponentView";

export class AppComponent extends CardView {
    private readonly alert: MessageAlert;
    private readonly appName: TextBlock;
    private readonly appTitle: TextBlock;
    private readonly appType: TextBlock;

    constructor(private readonly hubApi: HubAppApi, view: AppComponentView) {
        super();
        new TextBlock('App', view.titleHeader);
        this.alert = new MessageAlert(view.alert);
        this.appName = new TextBlock('', view.appName);
        this.appTitle = new TextBlock('', view.appTitle);
        this.appType = new TextBlock('', view.appType);
    }

    async refresh() {
        let app = await this.getApp();
        this.appName.setText(app.AppName);
        this.appTitle.setText(app.Title);
        this.appType.setText(app.Type.DisplayText);
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