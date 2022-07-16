import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { AppListPanel } from './AppListPanel';
import { MainPageView } from './MainPageView';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly appListPanel: AppListPanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.appListPanel = new AppListPanel(this.hubApi, this.view.appListPanel);
        this.activateAppListPanel();
    }

    private async activateAppListPanel() {
        this.appListPanel.refresh();
        const result = await this.appListPanel.start();
        if (result.appSelected) {
            this.hubApi.Apps.RedirectToApp.open(result.appSelected.app.ID);
        }
    }
}
new MainPage();