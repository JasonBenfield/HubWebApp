import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { IMessageAlert } from "@jasonbenfield/sharedwebapp/Components/Types";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { App } from "../../../Lib/App";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { XtiVersion } from "../../../Lib/XtiVersion";
import { AppCardView } from "./AppCardView";

export class AppCard {
    private readonly alert: IMessageAlert;
    private readonly appKeyTextComponent: TextComponent;
    private readonly versionKeyFormGroup: FormGroupText;
    private readonly versionNumberFormGroup: FormGroupText;
    private app = new App();
    private version = new XtiVersion();

    constructor(private readonly hubClient: HubAppClient, view: AppCardView) {
        this.alert = new CardAlert(view.alert);
        this.appKeyTextComponent = new TextComponent(view.appKeyTextView);
        this.appKeyTextComponent.setText("App");
        this.versionKeyFormGroup = new FormGroupText(view.versionKeyFormGroupView);
        this.versionKeyFormGroup.setCaption("Version Key");
        this.versionNumberFormGroup = new FormGroupText(view.versionNumberFormGroupView);
        this.versionNumberFormGroup.setCaption("Version Number");
    }

    async refresh() {
        this.versionKeyFormGroup.hide();
        this.versionNumberFormGroup.hide();
        const promises = [
            this.refreshApp(),
            this.refreshVersion()
        ];
        await this.alert.infoAction(
            "Loading...",
            () => Promise.all(promises)
        );
        this.appKeyTextComponent.setText(this.app.appKey.format());
        this.versionKeyFormGroup.setValue(this.version.versionKey.displayText);
        this.versionKeyFormGroup.show();
        this.versionNumberFormGroup.setValue(this.version.versionNumber.format());
        this.versionNumberFormGroup.show();
        return {
            app: this.app,
            version: this.version
        }
    }

    private async refreshApp() {
        const sourceApp = await this.hubClient.App.GetApp();
        this.app = new App(sourceApp);
    }

    private async refreshVersion() {
        const sourceCurrentVersion = await this.alert.infoAction(
            "Loading...",
            () => this.hubClient.Version.GetVersion("current")
        );
        this.version = new XtiVersion(sourceCurrentVersion);
    }
}