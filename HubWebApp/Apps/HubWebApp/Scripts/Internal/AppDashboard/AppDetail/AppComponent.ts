import { CardAlert } from '@jasonbenfield/sharedwebapp/Components/CardAlert';
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from '@jasonbenfield/sharedwebapp/Components/TextComponent';
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { AppComponentView } from "./AppComponentView";

export class AppComponent {
    private readonly alert: MessageAlert;
    private readonly appName: TextComponent;
    private readonly appTitle: TextComponent;
    private readonly appType: TextComponent;

    constructor(private readonly hubApi: HubAppApi, view: AppComponentView) {
        new TextComponent(view.titleHeader).setText('App');
        this.alert = new CardAlert(view.alert).alert;
        this.appName = new TextComponent(view.appName);
        this.appTitle = new TextComponent(view.appTitle);
        this.appType = new TextComponent(view.appType);
    }

    async refresh() {
        const app = await this.getApp();
        this.appName.setText(app.AppKey.Name.DisplayText);
        this.appTitle.setText(app.Title);
        this.appType.setText(app.AppKey.Type.DisplayText);
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