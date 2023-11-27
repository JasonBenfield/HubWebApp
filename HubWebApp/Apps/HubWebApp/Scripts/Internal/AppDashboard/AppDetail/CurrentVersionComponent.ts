import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppClient } from "../../../Lib/Http/HubAppClient";
import { CurrentVersionComponentView } from "./CurrentVersionComponentView";
import { XtiVersion } from "../../../Lib/XtiVersion";

export class CurrentVersionComponent {
    private readonly alert: MessageAlert;
    private readonly versionKey: TextComponent;
    private readonly version: TextComponent;

    constructor(private readonly hubClient: HubAppClient, view: CurrentVersionComponentView) {
        new TextComponent(view.titleHeader).setText('Version');
        this.alert = new MessageAlert(view.alert);
        this.versionKey = new TextComponent(view.versionKey);
        this.version = new TextComponent(view.version);
    }

    async refresh() {
        const sourceCurrentVersion = await this.getCurrentVersion();
        const currentVersion = new XtiVersion(sourceCurrentVersion);
        this.versionKey.setText(currentVersion.versionKey.displayText);
        this.version.setText(currentVersion.versionNumber.format());
    }

    private getCurrentVersion() {
        return this.alert.infoAction(
            'Loading...',
            () => this.hubClient.Version.GetVersion('current')
        );
    }
} 