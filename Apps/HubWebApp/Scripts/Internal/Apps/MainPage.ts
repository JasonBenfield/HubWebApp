import 'reflect-metadata';
import { startup } from 'xtistart';
import { singleton } from 'tsyringe';
import { MainPageViewModel } from './MainPageViewModel';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { AppListPanel } from './AppListPanel';

@singleton()
class MainPage {
    constructor(
        private readonly vm: MainPageViewModel,
        private readonly hubApi: HubAppApi
    ) {
        this.activateAppListPanel();
    }

    private readonly appListPanel = new AppListPanel(this.vm.appListPanel, this.hubApi);

    private async activateAppListPanel() {
        this.appListPanel.refresh();
        let result = await this.appListPanel.start();
        if (result.key === AppListPanel.ResultKeys.appSelected) {
            let app: IAppModel = result.data;
            this.hubApi.Apps.RedirectToApp.open(app.ID);
        }
    }
}
startup(MainPageViewModel, MainPage);