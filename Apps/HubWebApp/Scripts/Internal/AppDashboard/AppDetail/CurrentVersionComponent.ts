import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { Alert } from "XtiShared/Alert";
import { CurrentVersionComponentViewModel } from "./CurrentVersionComponentViewModel";

export class CurrentVersionComponent {
    constructor(
        private readonly vm: CurrentVersionComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }
    readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let currentVersion = await this.getCurrentVersion();
        this.vm.versionKey(currentVersion.VersionKey);
        this.vm.version(`${currentVersion.Major}.${currentVersion.Minor}.${currentVersion.Patch}`);
    }

    private async getCurrentVersion() {
        let currentVersion: IAppVersionModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                currentVersion = await this.hubApi.App.GetCurrentVersion();
            }
        );
        return currentVersion;
    }
} 