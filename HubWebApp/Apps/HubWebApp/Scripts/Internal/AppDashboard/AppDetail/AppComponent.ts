﻿import { CardAlert } from '@jasonbenfield/sharedwebapp/Components/CardAlert';
import { TextComponent } from '@jasonbenfield/sharedwebapp/Components/TextComponent';
import { IMessageAlert } from '@jasonbenfield/sharedwebapp/Components/Types';
import { App } from '../../../Lib/App';
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { AppComponentView } from "./AppComponentView";

export class AppComponent {
    private readonly alert: IMessageAlert;
    private readonly appName: TextComponent;
    private readonly appTitle: TextComponent;
    private readonly appType: TextComponent;

    constructor(private readonly hubClient: HubAppClient, view: AppComponentView) {
        new TextComponent(view.titleHeader).setText('App');
        this.alert = new CardAlert(view.alert);
        this.appName = new TextComponent(view.appName);
        this.appTitle = new TextComponent(view.appTitle);
        this.appType = new TextComponent(view.appType);
    }

    async refresh() {
        const sourceApp = await this.getApp();
        const app = new App(sourceApp);
        this.appName.setText(app.appKey.name.displayText);
        this.appTitle.setText(`${app.appKey.format()}`);
        this.appType.setText(app.appKey.type.DisplayText);
    }

    private getApp() {
        return this.alert.infoAction(
            'Loading...',
            async () => this.hubClient.App.GetApp()
        );
    }
}