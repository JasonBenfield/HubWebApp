import { PaddingCss } from '@jasonbenfield/sharedwebapp/PaddingCss';
import { PageFrameView } from '@jasonbenfield/sharedwebapp/PageFrameView';
import { Startup } from '@jasonbenfield/sharedwebapp/Startup';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { Apis } from '../../Hub/Apis';
import { AppListPanel } from './AppListPanel';
import { MainPageView } from './MainPageView';

class MainPage {
    private readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly appListPanel: AppListPanel;

    constructor(private readonly page: PageFrameView) {
        this.view = new MainPageView(page);
        this.hubApi = new Apis(this.page.modalError).hub();
        this.page.content.setPadding(PaddingCss.top(3));
        this.appListPanel = new AppListPanel(this.hubApi, this.view.appListPanel);
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