import { Startup } from 'xtistart';
import { HubAppApi } from '../../Hub/Api/HubAppApi';
import { AppListPanel } from './AppListPanel';
import { PageFrame } from 'XtiShared/PageFrame';
import { PaddingCss } from 'XtiShared/PaddingCss';

class MainPage {
    constructor(private readonly page: PageFrame) {
        this.page.content.setPadding(PaddingCss.top(3));
        this.activateAppListPanel();
    }

    private readonly hubApi = this.page.api(HubAppApi);
    private readonly appListPanel = this.page.content.addContent(new AppListPanel(this.hubApi));

    private async activateAppListPanel() {
        this.appListPanel.refresh();
        let result = await this.appListPanel.start();
        if (result.key === AppListPanel.ResultKeys.appSelected) {
            let app: IAppModel = result.data;
            this.hubApi.Apps.RedirectToApp.open(app.ID);
        }
    }
}
new MainPage(new Startup().build());