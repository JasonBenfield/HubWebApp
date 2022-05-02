import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../Apis';
import { AppListPanel } from './AppListPanel';
import { MainPageView } from './MainPageView';

class MainPage {
    private readonly hubApi: HubAppApi;
    private readonly appListPanel: AppListPanel;

    constructor(page: PageFrameView) {
        let view = new MainPageView(page);
        this.hubApi = new Apis(page.modalError).Hub();
        page.content.setPadding(PaddingCss.top(3));
        this.appListPanel = new AppListPanel(this.hubApi, view.appListPanel);
        this.activateAppListPanel();
    }

    private async activateAppListPanel() {
        this.appListPanel.refresh();
        let result = await this.appListPanel.start();
        if (result.appSelected) {
            this.hubApi.Apps.RedirectToApp.open(result.appSelected.app.ID);
        }
    }
}
new MainPage(new Startup().build());