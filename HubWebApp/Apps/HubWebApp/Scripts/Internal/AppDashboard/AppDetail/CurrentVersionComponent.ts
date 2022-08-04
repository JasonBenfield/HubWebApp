import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { CurrentVersionComponentView } from "./CurrentVersionComponentView";

export class CurrentVersionComponent {
    private readonly alert: MessageAlert;
    private readonly versionKey: TextComponent;
    private readonly version: TextComponent;

    constructor(private readonly hubApi: HubAppApi, view: CurrentVersionComponentView) {
        new TextComponent(view.titleHeader).setText('Version');
        this.alert = new MessageAlert(view.alert);
        this.versionKey = new TextComponent(view.versionKey);
        this.version = new TextComponent(view.version);
    }

    async refresh() {
        const currentVersion = await this.getCurrentVersion();
        this.versionKey.setText(currentVersion.VersionKey.DisplayText);
        this.version.setText(
            `${currentVersion.VersionNumber.Major}.${currentVersion.VersionNumber.Minor}.${currentVersion.VersionNumber.Patch}`
        );
    }

    private async getCurrentVersion() {
        let currentVersion: IXtiVersionModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                currentVersion = await this.hubApi.Version.GetVersion('current');
            }
        );
        return currentVersion;
    }
} 