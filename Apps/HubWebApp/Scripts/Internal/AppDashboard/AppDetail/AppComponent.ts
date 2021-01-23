import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { Alert } from "XtiShared/Alert";
import { AppComponentViewModel } from "./AppComponentViewModel";

export class AppComponent {
    constructor(
        private readonly vm: AppComponentViewModel,
        private readonly hubApi: HubAppApi
    ) {
    }

    readonly alert = new Alert(this.vm.alert);

    async refresh() {
        let app = await this.getApp();
        this.vm.appName(app.AppName);
        this.vm.title(app.Title);
        this.vm.appType(app.Type.DisplayText);
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