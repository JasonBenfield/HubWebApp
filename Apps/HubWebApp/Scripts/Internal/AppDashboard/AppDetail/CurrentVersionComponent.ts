import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { CurrentVersionComponentView } from "./CurrentVersionComponentView";

export class CurrentVersionComponent {
    private readonly alert: MessageAlert;

    constructor(private readonly hubApi: HubAppApi, private readonly view: CurrentVersionComponentView) {
        new CardTitleHeader('Version', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    async refresh() {
        let currentVersion = await this.getCurrentVersion();
        this.view.setVersionKey(currentVersion.VersionKey);
        this.view.setVersion(`${currentVersion.Major}.${currentVersion.Minor}.${currentVersion.Patch}`);
    }

    private async getCurrentVersion() {
        let currentVersion: IAppVersionModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                currentVersion = await this.hubApi.Version.GetVersion('current');
            }
        );
        return currentVersion;
    }
} 