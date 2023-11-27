import { WebPage } from '@jasonbenfield/sharedwebapp/Http/WebPage';
import { SingleActivePanel } from '@jasonbenfield/sharedwebapp/Panel/SingleActivePanel';
import { HubPage } from '../HubPage';
import { MainMenuPanel } from '../MainMenuPanel';
import { AppListPanel } from './AppListPanel';
import { MainPageView } from './MainPageView';

class MainPage extends HubPage {
    protected readonly view: MainPageView;
    private readonly panels = new SingleActivePanel();
    private readonly appListPanel: AppListPanel;
    private readonly mainMenuPanel: MainMenuPanel;

    constructor() {
        super(new MainPageView());
        this.appListPanel = this.panels.add(
            new AppListPanel(this.hubClient, this.view.appListPanel)
        );
        this.mainMenuPanel = this.panels.add(
            new MainMenuPanel(this.hubClient, this.view.mainMenuPanel)
        );
        this.appListPanel.refresh();
        this.activateAppListPanel();
    }

    private async activateAppListPanel() {
        this.panels.activate(this.appListPanel);
        const result = await this.appListPanel.start();
        if (result.appSelected) {
            const url = this.hubClient.Apps.Index.getModifierUrl(
                result.appSelected.app.getModifier(),
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