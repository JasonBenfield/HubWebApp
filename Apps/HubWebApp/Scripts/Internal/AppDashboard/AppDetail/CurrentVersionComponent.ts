import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { CurrentVersionComponentView } from "./CurrentVersionComponentView";

export class CurrentVersionComponent {
    private readonly alert: MessageAlert;
    private readonly versionKey: TextBlock;
    private readonly version: TextBlock;

    constructor(private readonly hubApi: HubAppApi, private readonly view: CurrentVersionComponentView) {
        new TextBlock('Version', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
        this.versionKey = new TextBlock('', this.view.versionKey);
        this.version = new TextBlock('', this.view.version);
    }

    async refresh() {
        let currentVersion = await this.getCurrentVersion();
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