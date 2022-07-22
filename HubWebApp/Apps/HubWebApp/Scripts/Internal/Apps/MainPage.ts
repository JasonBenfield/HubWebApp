import { BasicPage } from '@jasonbenfield/sharedwebapp/Components/BasicPage';
import { WebPage } from '@jasonbenfield/sharedwebapp/Api/WebPage';
import { HubAppApi } from '../../Lib/Api/HubAppApi';
import { Apis } from '../Apis';
import { AppListPanel } from './AppListPanel';
import { MainPageView } from './MainPageView';
import { MainMenuPanel } from '../MainMenuPanel';
import { SingleActivePanel } from '../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Panel/SingleActivePanel';

class MainPage extends BasicPage {
    protected readonly view: MainPageView;
    private readonly hubApi: HubAppApi;
    private readonly panels = new SingleActivePanel();
    private readonly appListPanel: AppListPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.hubApi = new Apis(this.view.modalError).Hub();
        this.appListPanel = this.panels.add(
            new AppListPanel(this.hubApi, this.view.appListPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubApi, this.view.mainMenuPanel)
        );
        this.appListPanel.refresh();
        this.activateAppListPanel();
    }

    private async activateAppListPanel() {
        this.panels.activate(this.appListPanel);
        const result = await this.appListPanel.start();
        if (result.appSelected) {
            const url = this.hubApi.Apps.Index.getModifierUrl(
                result.appSelected.app.PublicKey.DisplayText,
                {}
            );
            new WebPage(url).open();
        }
        else if (result.mainMenuRequested) {
            this.activateMainMenuPanel();
        }
    }

    private async activateMainMenuPanel() {
        this.panels.activate(this.mainMenuPanel);
        const result = await this.mainMenuPanel.start();
        if (result.back) {
            this.activateAppListPanel();
        }
    }
}
new MainPage();